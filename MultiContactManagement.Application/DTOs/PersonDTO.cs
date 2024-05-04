using MultiContactManagement.Domain.Entities;

namespace MultiContactManagement.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
