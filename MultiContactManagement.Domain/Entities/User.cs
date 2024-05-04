using MultiContactManagement.Domain.Validation;

namespace MultiContactManagement.Domain.Entities
{
    public class User : Base
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public User(int id, string name, string email)
        {
            DomainExceptionValidation.When(id < 0, "The user's ID has not been found.");
            Id = id;
            ValidateDomain(name, email);
        }

        public void SetAdmin(bool isAdmin)
        {
            IsAdmin = isAdmin;
        }

        public void ChangePassword(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void ValidateDomain(string name, string email)
        {
            DomainExceptionValidation.When(name == null, "Name is required.");
            DomainExceptionValidation.When(email == null, "Email is required.");
            DomainExceptionValidation.When(name.Length > 100, "Name cannot exceed 250 characters.");
            DomainExceptionValidation.When(email.Length > 100, "E-mail cannot exceed 200 characters.");
            Name = name;
            Email = email;
            IsAdmin = false;
        }
    }
}
