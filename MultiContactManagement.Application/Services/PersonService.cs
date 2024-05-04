using AutoMapper;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;

namespace MultiContactManagement.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<PersonDTO> Create(PersonDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            var personRegistered = await _personRepository.Create(person);
            return _mapper.Map<PersonDTO>(personRegistered);
        }

        public async Task<PersonDTO> Update(PersonDTO personDTO)
        {
            var person = _mapper.Map<Person>(personDTO);
            var updatedPerson = await _personRepository.Update(person);
            return _mapper.Map<PersonDTO>(updatedPerson);
        }

        public async Task<PersonDTO> Delete(int id)
        {
            var person = await _personRepository.GetAsync(id);
            if (person == null)
            {
                throw new ArgumentException("Person not found", nameof(id));
            }
            await _personRepository.Delete(id);
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<IEnumerable<PersonDTO>> GetAllPeopleAsync()
        {
            var people = await _personRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PersonDTO>>(people);
        }

        public async Task<PersonDTO> GetPersonAsync(int id)
        {
            var person = await _personRepository.GetAsync(id);
            return _mapper.Map<PersonDTO>(person);
        }
    }
}
