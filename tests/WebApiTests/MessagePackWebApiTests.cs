using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Components.Http;
using Components.MsgPack;
using MessagePack;
using Models; 
using Xunit;

namespace WebApiTests
{
    public class MessagePackWebApiTests : IClassFixture<WebApiHostFactory<MessagePackWebApi.Startup>>
    {

        HttpClient client;
        public MessagePackWebApiTests(WebApiHostFactory<MessagePackWebApi.Startup> factory)
        {
            var options = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions()
            {
                 BaseAddress = new Uri("http://localhost"),
                AllowAutoRedirect = false,
                HandleCookies = false,
                MaxAutomaticRedirections = 0

            };

            client = factory.CreateClient(options);
            
        }

        [Fact]
        public async Task PostProduct()
        {
            var testsFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var imageUrl = testsFolder+"\\laptopImage.jpg";
            var imageBytes = System.IO.File.ReadAllBytes(imageUrl);
            Product product = new Product()
            {
                Id = 1,
                Category = Models.Category.Electronic,
                Created = DateTime.UtcNow,
                Description = @"o beyond the traditional laptop with Surface Laptop. Backed by the best of Microsoft, including Windows3 and Office5. Enjoy a natural typing experience enhanced by our Signature Alcantara® fabric-covered keyboard. Thin, light, and powerful, it fits easily in your bag",
                Image = imageBytes,
                Labels = new List<string> { "windows", "laptop", "gray" },
                Location = new Uri("http://microsoft.pt"),
                Name = "surface pro",
                Price = Decimal.Parse("959,32"),

                Rating = Models.Rate.Good,
                Details = new Dictionary<string, string>()
                  {
                      { "SO","windows 10" },
                      { "CPU","i5" },
                      { "RAM","4GB" },
                      { "Storage","128GB" },
                      { "BatteryLife","14.5h" },
                  }

            };


            DefaultFormatterResolver.Add(Models.Formatters.ModelsFormatterResolver.Instance);

            
              client.DefaultRequestHeaders.Add("Accept", "application/msg-pack");
            var response = await client.PostAsync("api/product", new MessagePackContent<Product>(product, DefaultFormatterResolver.Instance));
          
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
        }


        [Fact]
        public async Task GetProduct()
        {
             
         
            client.DefaultRequestHeaders.Add("Accept", "application/msg-pack");
            var response = await client.GetAsync("api/product/199999",HttpCompletionOption.ResponseContentRead);

             
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }
    }
}
