using Microsoft.EntityFrameworkCore;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;
using MultiContactManagement.Infra.Context;

namespace MultiContactManagement.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Contact> Create(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> Update(Contact contact)
        {
            _context.Contact.Update(contact);
            await _context.SaveChangesAsync();
            return contact;
        }

        public async Task<Contact> Delete(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
                return contact;
            }

            return null;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contact.ToListAsync();
        }

        public async Task<Contact> GetAsync(int id)
        {
            return await _context.Contact.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
