using Microsoft.AspNetCore.Mvc;
using MultiContactManagement.API.Models;
using MultiContactManagement.Domain.Account;

namespace MultiContactManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login(LoginModel loginModel)
        {
            var exists = await _authenticateService.UserExists(loginModel.Email);
            if (!exists)
            {
                return Unauthorized("User doesn't exist");
            }

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (!result)
            {
                return Unauthorized("Invalid user or password.");
            }

            var user = await _authenticateService.GetUserByEmail(loginModel.Email);

            var token = _authenticateService.GenerateToken(user.Id, user.Email);

            return new UserToken
            {
                Token = token
            };
        }
    }
}
