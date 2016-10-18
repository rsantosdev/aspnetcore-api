using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.WebApp.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
