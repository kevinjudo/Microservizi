using UserService.Repository.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Repository.Abstraction
{
    // Interfaccia per definire le operazioni di gestione degli utenti nel database
    public interface IUserRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task CreateUserAsync(User user, CancellationToken cancellationToken = default);

        Task<User?> ReadUserAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        void UpdateUser(User user); 
        void DeleteUser(User user); 
    }
}
