

using System.Net.Http;
using System.Web.Http;
using Components.Web.Http.Tests.Models;

namespace Components.Web.Http.Tests
{
    public class ProductController : ApiController
    {
        public ProductController()
        {

        }


        [HttpPost]
        public HttpResponseMessage AddClient([FromBody]Person client)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.Created)
            {
                Content = new StringContent($"{this.ModelState.IsValid}"),
            };
        }


        [HttpPost]
        public HttpResponseMessage CreateProduct([FromBody]Product product)
        {

            return new HttpResponseMessage(System.Net.HttpStatusCode.Created)
            {
                Content = new StringContent($"{this.ModelState.IsValid}"),
            };
        }
    }
}
