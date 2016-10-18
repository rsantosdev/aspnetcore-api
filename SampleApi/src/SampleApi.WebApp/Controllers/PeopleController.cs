using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleApi.WebApp.Models;

namespace SampleApi.WebApp.Controllers
{
    [Route("api/people")]
    public class PeopleController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Json(PeopleRepository.People.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var person = PeopleRepository.People.FirstOrDefault(x => x.Id == id);
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
            PeopleRepository.People.Add(person);

            return Ok(person);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Person updated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = PeopleRepository.People.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            PeopleRepository.People.Remove(person);

            updated.Id = id;
            PeopleRepository.People.Add(updated);

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var person = PeopleRepository.People.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            PeopleRepository.People.Remove(person);

            return Ok();
        }
    }
}
