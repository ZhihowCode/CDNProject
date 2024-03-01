using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CDN.Application.DTOs;
using CDN.Application.Interfaces;
using CDN.Domain.Entities;
using CDN.Domain.Interfaces;

namespace CDN.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(
                user =>
                    new UserDto
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Mail = user.Mail,
                        PhoneNumber = user.PhoneNumber,
                        Skillsets = user.Skillsets,
                        Hobby = user.Hobby
                    }
            );
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Mail = user.Mail,
                PhoneNumber = user.PhoneNumber,
                Skillsets = user.Skillsets,
                Hobby = user.Hobby
            };
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var newUser = new User
            {
                Username = userDto.Username,
                Mail = userDto.Mail,
                PhoneNumber = userDto.PhoneNumber,
                Skillsets = userDto.Skillsets,
                Hobby = userDto.Hobby
            };

            await _userRepository.AddAsync(newUser);

            userDto.Id = newUser.Id; // Assuming the ID is auto-generated by the database
            return userDto;
        }

        public async Task UpdateUserAsync(int id, UserDto userDto)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(id);
            if (userToUpdate == null)
                return;

            userToUpdate.Username = userDto.Username;
            userToUpdate.Mail = userDto.Mail;
            userToUpdate.PhoneNumber = userDto.PhoneNumber;
            userToUpdate.Skillsets = userDto.Skillsets;
            userToUpdate.Hobby = userDto.Hobby;

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
