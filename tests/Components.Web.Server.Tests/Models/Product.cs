using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;

namespace Components.Web.Server.Tests.Models
{
    public enum Category
    {
        Retail,
        Electronic,
        Realstate,
        Vehicle,

    }

    public enum Rate
    {
        Horrible,
        Bad,
        Good,
        Great,
        Awsome,

    }

    [MessagePackObject(keyAsPropertyName: true)]
    public class Product
    {


        public Product()
        {
            this.Labels = new List<string>();
            this.Details = new Dictionary<string, string>();
            this.Created = DateTime.UtcNow;
        }

        public long Id { get; set; }

        public byte[] Image { get; set; }

        public string Seller { get; set; }

        public string Name  { get; set; }

        public Rate Rating { get; set; } 

        public Category Category { get; set; }
                

        public decimal? Price { get; set; }

        public string Currency { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }


        public String Description { get; set; }

        public Uri Location { get; set; }
        
        public ICollection<string> Labels { get; set; }
        
        public Dictionary<string, string> Details { get; set; }
        
    }
}
