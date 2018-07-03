using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Components.Web.Server.Tests.Models;

namespace Components.Web.Server.Tests.Services
{

    public interface IProductsProvider
    {
        Task<bool> Create(Product product);
        Task<Product> Get(long id);

        Task<IEnumerable<Product>> GetMany(Category? category, IEnumerable<string> labels, decimal? lowerPrice, decimal? higherPrice, int skip, int top);

    }


    public class InMemoryProductProvider : IProductsProvider
    {
        List<Product> products = new List<Product>();

        public Task<bool> Create(Product product)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            products.Add(product);

            tcs.SetResult(true);

            return tcs.Task;
        }

        public Task<Product> Get(long id)
        {
            TaskCompletionSource<Product> tcs = new TaskCompletionSource<Product>();

            var productFound = products.FirstOrDefault((product) => product.Id == id);

            tcs.SetResult(productFound);

            return tcs.Task;
        }

        public Task<IEnumerable<Product>> GetMany(Category? category, IEnumerable<string> labels, decimal? lowerPrice, decimal? higherPrice, int skip, int top)
        {
            TaskCompletionSource<IEnumerable<Product>> tcs = new TaskCompletionSource<IEnumerable<Product>>();
            
            Func<Product, bool> EvaluateCategory = (product) => category.HasValue ? product.Category == category : true;
            Func<Product, bool> EvaluateLabels = (product) => labels == null || labels != null && labels.Count() == 0 ?  true: product.Labels.Any((lb) => labels.Contains(lb));
            Func<Product, bool> EvaluatePrice = (product) => product.Price.HasValue ? ((!lowerPrice.HasValue ? true : product.Price > lowerPrice.Value) && (!higherPrice.HasValue ? true : product.Price < higherPrice.Value)) : true; 
            
            IEnumerable<Product> result = products.FindAll((product) => EvaluateCategory(product) || EvaluateLabels(product) || EvaluatePrice(product));

            tcs.SetResult(result.Skip(skip).Take(top));

            return tcs.Task;
            
        }
    }
}
