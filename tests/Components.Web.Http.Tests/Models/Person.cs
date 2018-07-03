using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.Web.Http.Tests.Models
{
    public class Person
    {
        [Required()]
        public string  CitizenId {get; set; }

        [Required()]
        public string Name { get; set; }
        public DateTime Birth { get; set; }
    }
}
