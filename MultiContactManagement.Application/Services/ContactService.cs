using AutoMapper;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;

namespace MultiContactManagement.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;

        public ContactService(IMapper mapper, IContactRepository contactRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
        }

        public async Task<ContactDTO> Create(ContactDTO contactDTO)
        {
            var contact = _mapper.Map<Contact>(contactDTO);
            var contactRegistered = await _contactRepository.Create(contact);
            return _mapper.Map<ContactDTO>(contactRegistered);
        }

        public async Task<ContactDTO> Update(ContactDTO contactDTO)
        {
            var contact = _mapper.Map<Contact>(contactDTO);
            var updatedContact = await _contactRepository.Update(contact);
            return _mapper.Map<ContactDTO>(updatedContact);
        }

        public async Task<ContactDTO> Delete(int id)
        {
            var contact = await _contactRepository.GetAsync(id);
            if (contact == null)
            {
                throw new ArgumentException("Contact not found", nameof(id));
            }
            await _contactRepository.Delete(id);
            return _mapper.Map<ContactDTO>(contact);
        }

        public async Task<IEnumerable<ContactDTO>> GetAllContactAsync()
        {
            var contacts = await _contactRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactDTO>>(contacts);
        }

        public async Task<ContactDTO> GetContactAsync(int id)
        {
            var contact = await _contactRepository.GetAsync(id);
            return _mapper.Map<ContactDTO>(contact);
        }
    }
}
