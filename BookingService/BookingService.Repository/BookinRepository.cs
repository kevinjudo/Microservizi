using BookingService.Repository.Abstraction;
using BookingService.Repository.Model;
using BookingService.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookingService.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public async Task CreateBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
        }

        public async Task<Booking?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }

        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
        }

        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
