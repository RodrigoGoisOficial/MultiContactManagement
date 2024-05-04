using Microsoft.EntityFrameworkCore;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;
using MultiContactManagement.Infra.Context;

namespace MultiContactManagement.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            if (user.PasswordSalt == null || user.PasswordHash == null)
            {
                var passwordCripgrafado = await _context.User.Where(x => x.Id == user.Id).Select(x => new { x.PasswordHash, x.PasswordSalt }).FirstOrDefaultAsync();
                user.ChangePassword(passwordCripgrafado.PasswordHash, passwordCripgrafado.PasswordSalt);
            }

            _context.User.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
