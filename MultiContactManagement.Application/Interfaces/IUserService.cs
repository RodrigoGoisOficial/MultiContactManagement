using MultiContactManagement.Application.DTOs;

namespace MultiContactManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> Create(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> Delete(int id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetAsync(int id);
    }
}
