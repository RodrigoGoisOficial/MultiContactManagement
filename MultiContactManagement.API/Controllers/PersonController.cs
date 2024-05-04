using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Account;

namespace MultiContactManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IAuthenticateService _authenticateService;

        public PersonController(IPersonService personService, IAuthenticateService authenticateService)
        {
            _personService = personService;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Create([FromBody] PersonDTO personDTO)
        {
            var createdPerson = await _personService.Create(personDTO);
            return Ok(createdPerson);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<PersonDTO>> Update(int id, [FromBody] PersonDTO personDTO)
        {
            if (id != personDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            var existingPerson = await _personService.GetPersonAsync(id);
            if (existingPerson == null)
            {
                return NotFound("Person not found");
            }

            var updatedPerson = await _personService.Update(personDTO);
            if (updatedPerson == null)
            {
                return BadRequest("An error occurred when updating the person.");
            }

            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var existingPerson = await _personService.GetPersonAsync(id);
            if (existingPerson == null)
            {
                return NotFound("Person not found");
            }

            await _personService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetAllPeople()
        {
            var people = await _personService.GetAllPeopleAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDTO>> GetPerson(int id)
        {
            var person = await _personService.GetPersonAsync(id);
            if (person == null)
            {
                return NotFound("Person not found");
            }

            return Ok(person);
        }
    }
}
