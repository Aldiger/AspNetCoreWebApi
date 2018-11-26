using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assecor.Services.Dtos;
using Assecor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AssecorTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }
        // GET api/Persons
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(_personsService.GetPersons());
        }

        [HttpGet("color/{color}.{format?}"), FormatFilter]
        public IActionResult GetByColor(string color)
        {
            return Ok(_personsService.GetPersonsByColor(color));
        }

        // GET api/Persons/5
        [HttpGet("{id}.{format?}"), FormatFilter]
        public IActionResult Get(int id)
        {
            return Ok(_personsService.GetPerson(id));
        }

        // POST api/Persons
        [HttpPost]
        public IActionResult Post([FromBody] PersonDto person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data are not valid!");
            }
            _personsService.AddPerson(person);
            return Ok("New element inserted successfully");
        }

        // PUT api/Persons/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonDto person)
        {
            if (!ModelState.IsValid || id !=person.Id)
            {
                return BadRequest("Data are not valid!");
            }
            _personsService.UpdatePerson(person);
            return Ok("New element inserted successfully");
        }

        // DELETE api/Persons/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _personsService.DeletePerson(id);
        }
    }
}
