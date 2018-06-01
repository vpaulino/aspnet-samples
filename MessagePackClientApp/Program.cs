using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessagePackClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("appAddr"));
            var postRoute = new Uri(ConfigurationManager.AppSettings.Get("postRoute"));
            var imageUrl = ConfigurationManager.AppSettings.Get("image");

            
            Models.Product product = new Models.Product()
            {
                Id = 1,
                Category = Models.Category.Electronic,
                Created = DateTime.UtcNow,
                Description = @"o beyond the traditional laptop with Surface Laptop. Backed by the best of Microsoft, including Windows3 and Office5. Enjoy a natural typing experience enhanced by our Signature Alcantara® fabric-covered keyboard. Thin, light, and powerful, it fits easily in your bag
",              Image = System.IO.File.ReadAllBytes(imageUrl),
                Labels = new List<string> { "windows", "laptop", "gray" },
                Location = new Uri("http://.microsoft.pt"),
                Name = "surface pro",
                Price = Decimal.Parse("959.32"),
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



             
        }
    }
}
