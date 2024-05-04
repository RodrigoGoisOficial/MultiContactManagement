using MultiContactManagement.Domain.Validation;

namespace MultiContactManagement.Domain.Entities
{
    public class Contact : Base
    {
        public string CountryCode { get; set; }
        public string Number { get; set; }
        public int PersonId { get; set; }

        public Contact(int id, string countryCode, string number)
        {
            DomainExceptionValidation.When(id < 0, "The contact  ID has not been found.");
            Id = id;
            ValidateDomain(countryCode, number);
        }

        public Contact(string countryCode, string number)
        {
            ValidateDomain(countryCode, number);
        }

        public void ValidateDomain(string countryCode, string number)
        {
            DomainExceptionValidation.When(countryCode == null, "Contact is required.");
            DomainExceptionValidation.When(number == null, "Number is required.");
            DomainExceptionValidation.When(number.Length < 10 || number.Length > 15, "Number must be between 10 and 15 characters.");
            CountryCode = countryCode;
            Number = number;
        }
    }
}
