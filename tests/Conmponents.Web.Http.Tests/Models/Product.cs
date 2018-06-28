using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Components.Web.Http.Tests.Models
{
    public class Product : Entity
    {
        [Required]
        public string Name { get; set; }

        [MinLength(1)]
        public ICollection<string> Tags { get; set; }


    }
}
