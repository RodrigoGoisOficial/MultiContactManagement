using MultiContactManagement.Domain.Validation;

namespace MultiContactManagement.Domain.Entities
{
    public class Person : Base
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Contact> Contacts { get; set; }

        public Person(int id, string name, string email)
        {
            DomainExceptionValidation.When(id < 0, "The person  ID has not been found.");
            Id = id;
            ValidateDomain(name, email);
        }

        public Person(string name, string email)
        {
            ValidateDomain(name, email);
        }

        public void ValidateDomain(string name, string email)
        {
            DomainExceptionValidation.When(name == null, "Name is required.");
            DomainExceptionValidation.When(email == null, "Email is required.");
            Name = name;
            Email = email;
        }
    }
}
