using UserService.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Business.Abstraction
{
    public interface IUserBusiness
    {
        Task CreateUserAsync(CreateUserDto dto, CancellationToken cancellationToken = default);
        Task<UserDto?> ReadUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteUserAsync(int userId, CancellationToken cancellationToken = default);
    }
}
