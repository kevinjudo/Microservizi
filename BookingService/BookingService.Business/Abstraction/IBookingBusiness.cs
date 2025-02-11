using BookingService.Shared;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookingService.Business.Abstraction
{
// Interfaccia per definire la logica di business delle prenotazioni
    public interface IBookingBusiness
    {
        Task CreateBookingAsync(CreateBookingDto dto, CancellationToken cancellationToken = default);
        Task<BookingDto?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default);
        Task<IEnumerable<BookingDto>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
        Task<BookingDto?> UpdateBookingAsync(UpdateBookingDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteBookingAsync(int bookingId, CancellationToken cancellationToken = default);

        Task<bool> UpdateUserForBookingAsync(int bookingId, int userId, CancellationToken cancellationToken = default);


    }
}
