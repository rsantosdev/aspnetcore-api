using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleApi.WebApp.Models;

namespace SampleApi.WebApp.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        private ICollection<Person> _people = new List<Person>();

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_people.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var person = _people.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound($"Person with id {id} was not found");
            }

            return Json(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            person.Id = Guid.NewGuid();
            _people.Add(person);

            return Ok(person);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Person updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = _people.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            _people.Remove(person);

            updated.Id = id;
            _people.Add(updated);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var person = _people.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            _people.Remove(person);

            return Ok();
        }
    }
}
