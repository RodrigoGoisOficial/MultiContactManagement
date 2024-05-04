using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Account;

namespace MultiContactManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IAuthenticateService _authenticateService;

        public ContactController(IContactService contactService, IAuthenticateService authenticateService)
        {
            _contactService = contactService;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ContactDTO>> Create([FromBody] ContactDTO contactDTO)
        {
            var createdContact = await _contactService.Create(contactDTO);
            return Ok(createdContact);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ContactDTO>> Update(int id, [FromBody] ContactDTO contactDTO)
        {
            if (id != contactDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            var existingContact = await _contactService.GetContactAsync(id);
            if (existingContact == null)
            {
                return NotFound("Contact not found");
            }

            var updatedContact = await _contactService.Update(contactDTO);
            if (updatedContact == null)
            {
                return BadRequest("An error occurred when updating the contact.");
            }

            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var existingContact = await _contactService.GetContactAsync(id);
            if (existingContact == null)
            {
                return NotFound("Contact not found");
            }

            await _contactService.Delete(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDTO>>> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContactAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContact(int id)
        {
            var contact = await _contactService.GetContactAsync(id);
            if (contact == null)
            {
                return NotFound("Contact not found");
            }

            return Ok(contact);
        }
    }
}
