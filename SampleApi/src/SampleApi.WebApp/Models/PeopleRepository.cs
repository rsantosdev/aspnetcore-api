using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.WebApp.Models
{
    public class PeopleRepository
    {
        public static ICollection<Person> People = new List<Person>();
    }
}
