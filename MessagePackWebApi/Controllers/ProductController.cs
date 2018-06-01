using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MessagePackWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/msg-pack")]
    public class ProductController : ControllerBase
    {
        IProductsProvider productsProvider;


        public ProductController(IProductsProvider provider)
        {
            this.productsProvider = provider;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            bool result = await productsProvider.Create(product);
            if (!result)
                return Conflict();

            return Created(UrlHelperExtensions.Action(Url, "GetProduct"), product);
        }
        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<IActionResult> Get(long id)
        {
            Product result = await productsProvider.Get(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetMany(Category? category, IEnumerable<string> labels, decimal? lowerPRice, decimal? higherPrice, int skip, int top)
        {
            IEnumerable<Product> result = await productsProvider.GetMany(category, labels, lowerPRice, higherPrice, skip, top);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}