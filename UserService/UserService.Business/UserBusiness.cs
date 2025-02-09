using UserService.Business.Abstraction;
using UserService.Repository.Abstraction;
using UserService.Repository.Model;
using UserService.Shared;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserBusiness> _logger;

        public UserBusiness(IUserRepository userRepository, ILogger<UserBusiness> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = DateTime.Parse(dto.DateOfBirth).Date
            };

            await _userRepository.CreateUserAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserDto?> ReadUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.ReadUserAsync(userId, cancellationToken);
            return user is not null ? new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd")
            } : null;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllUsersAsync(cancellationToken);
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth.ToString("yyyy-MM-dd")
            });
        }

        public async Task<UserDto?> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.ReadUserAsync(dto.Id, cancellationToken);
            if (existingUser is null) return null;

            existingUser.FirstName = dto.FirstName;
            existingUser.LastName = dto.LastName;
            existingUser.Email = dto.Email;
            existingUser.PhoneNumber = dto.PhoneNumber;
            existingUser.DateOfBirth = DateTime.Parse(dto.DateOfBirth).Date;

            _userRepository.UpdateUser(existingUser);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                Email = existingUser.Email,
                PhoneNumber = existingUser.PhoneNumber,
                DateOfBirth = existingUser.DateOfBirth.ToString("yyyy-MM-dd")
            };
        }

        public async Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.ReadUserAsync(userId, cancellationToken);
            if (user is null) return false;

            _userRepository.DeleteUser(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
