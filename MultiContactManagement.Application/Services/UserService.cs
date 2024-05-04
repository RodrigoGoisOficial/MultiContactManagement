using AutoMapper;
using MultiContactManagement.Application.DTOs;
using MultiContactManagement.Application.Interfaces;
using MultiContactManagement.Domain.Entities;
using MultiContactManagement.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MultiContactManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Create(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);

            if (userDTO.Password != null)
            {
                using var hmac = new HMACSHA512();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                byte[] passwordSalt = hmac.Key;

                user.ChangePassword(passwordHash, passwordSalt);
            }

            var userRegistered = await _userRepository.Create(user);

            return _mapper.Map<UserDTO>(userRegistered);
        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var updatedUser = await _userRepository.Update(user);

            return _mapper.Map<UserDTO>(updatedUser);
        }

        

        public async Task<UserDTO> Delete(int id)
        {
            var user = await _userRepository.Delete(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
