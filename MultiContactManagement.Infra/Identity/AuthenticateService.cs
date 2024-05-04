using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiContactManagement.Domain.Account;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Infra.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MultiContactManagement.Infra.Identity
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var user = await _context.User.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for (int x = 0; x < computedHash.Length; x++)
            {
                if (computedHash[x] != user.PasswordHash[x]) return false;
            }

            return true;

        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration["jwt:secretKey"]));

            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
             );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.User.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _context.User.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
