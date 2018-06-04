using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Contracts
{
    public interface IProductsProvider
    {
        Task<Product> Get(long id);

        Task<bool> Create(Product product);

        Task<IEnumerable<Product>> GetMany(Category? category, IEnumerable<string> labels, decimal? lowerPRice, decimal? higherPrice, int skip, int top);


    }
}
