using MultiContactManagement.Application.DTOs;

namespace MultiContactManagement.Application.Interfaces
{
    public interface IContactService
    {
        Task<ContactDTO> Create(ContactDTO contactDTO);
        Task<ContactDTO> Update(ContactDTO contactDTO);
        Task<ContactDTO> Delete(int id);
        Task<IEnumerable<ContactDTO>> GetAllContactAsync();
        Task<ContactDTO> GetContactAsync(int id);
    }
}
