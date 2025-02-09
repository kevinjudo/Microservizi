using System.Threading.Tasks;
using BookingService.Shared;

namespace BookingService.ClientHttp.Abstraction
{
    public interface IClientHttp
    {
        Task<UserDto?> GetUserByIdAsync(int userId);
    }
}
