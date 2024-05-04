using MultiContactManagement.Application.DTOs;

namespace MultiContactManagement.Application.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDTO> Create(PersonDTO personDTO);
        Task<PersonDTO> Update(PersonDTO personDTO);
        Task<PersonDTO> Delete(int id);
        Task<IEnumerable<PersonDTO>> GetAllPeopleAsync();
        Task<PersonDTO> GetPersonAsync(int id);
    }
}
