using Microsoft.EntityFrameworkCore;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;
using MultiContactManagement.Infra.Context;

namespace MultiContactManagement.Infra.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Person> Create(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> Update(Person person)
        {
            _context.Person.Update(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<Person> Delete(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
                return person;
            }

            return null;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Person.ToListAsync();
        }

        public async Task<Person> GetAsync(int id)
        {
            return await _context.Person.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
