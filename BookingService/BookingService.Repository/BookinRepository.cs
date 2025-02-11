using BookingService.Repository.Abstraction;
using BookingService.Repository.Model;
using BookingService.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookingService.Repository
{
    // Implementazione del repository per la gestione delle prenotazioni
    public class BookingRepository : IBookingRepository
    {
        private readonly BookingDbContext _context;
        // Costruttore che inietta il contesto del database
        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }
        // Metodo per creare una nuova prenotazione nel database
        public async Task CreateBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            await _context.Bookings.AddAsync(booking, cancellationToken);
        }
        // Metodo per leggere una singola prenotazione inserendo l'id
        public async Task<Booking?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken);
        }
        // Metodo per leggere tutte le prenotazione 
        public async Task<IEnumerable<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }
        // Metodo per modificare una prenotazione
        public void UpdateBooking(Booking booking)
        {
            _context.Bookings.Update(booking);
        }
        // Metodo per eliminare una prenotazione
        public void DeleteBooking(Booking booking)
        {
            _context.Bookings.Remove(booking);
        }
        // Metodo per salvare le modifiche nel database
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
