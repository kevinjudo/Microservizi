using BookingService.Repository.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookingService.Repository.Abstraction
{
    public interface IBookingRepository
    {
        Task CreateBookingAsync(Booking booking, CancellationToken cancellationToken = default);
        Task<Booking?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
