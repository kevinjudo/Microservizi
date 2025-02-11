using UserService.Repository.Model;
using UserService.Repository.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Repository
{
    // Implementazione del repository per la gestione degli utenti
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        // Costruttore che inietta il contesto del database
        public UserRepository(UserDbContext context)
        {
            _context = context;
        }
        // Vari metodi per creare un nuivo utente, leggerli, modificarli o eliminarli
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
        }


        public async Task<User?> ReadUserAsync(int userId, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }
    }
}
