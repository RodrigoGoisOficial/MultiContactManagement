using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiContactManagement.API.Models;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Account;

namespace MultiContactManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserService _userService;

        public UserController(IAuthenticateService authenticateService, IUserService userService)
        {
            _authenticateService = authenticateService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Create([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest("Invalid data");
            }

            var emailExists = await _authenticateService.UserExists(userDTO.Email);

            if (emailExists)
            {
                return BadRequest("This e-mail address is already registered");
            }

            var user = await _userService.Create(userDTO);
            if (user == null)
            {
                return BadRequest("An error occurred when registering the user.");
            }

            var token = _authenticateService.GenerateToken(user.Id, user.Email);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> Update(int id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            var existingUser = await _userService.GetAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var updatedUser = await _userService.Update(userDTO);
            if (updatedUser == null)
            {
                return BadRequest("An error occurred when updating the user.");
            }

            return updatedUser;
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var existingUser = await _userService.GetAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            var deletedUser = await _userService.Delete(id);
            if (deletedUser == null)
            {
                return BadRequest("An error occurred when deleting the user.");
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            var user = await _userService.GetAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
