using System.Threading.Tasks;
using BookingService.Shared;

namespace BookingService.ClientHttp.Abstraction
{
    // Interfaccia per la gestione delle chiamate HTTP ai microservizi
    public interface IClientHttp
    {
        Task<UserDto?> GetUserByIdAsync(int userId); // Metodo per ottenere un utente tramite ID dal servizio utenti
    }
}
