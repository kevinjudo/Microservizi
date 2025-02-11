using BookingService.Business.Abstraction;
using BookingService.Repository.Abstraction;
using BookingService.Repository.Model;
using BookingService.Shared;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookingService.ClientHttp.Abstraction;

namespace BookingService.Business
{
    public class BookingBusiness : IBookingBusiness
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingBusiness> _logger;
        private readonly IClientHttp _clientHttp; // Interfaccia per chiamate HTTP verso UserService
        // Costruttore che inietta il repository, il client HTTP e il logger
        public BookingBusiness(IBookingRepository bookingRepository, IClientHttp clientHttp, ILogger<BookingBusiness> logger)
        {
            _bookingRepository = bookingRepository;
            _clientHttp = clientHttp;
            _logger = logger;

        }
        // Metodo per creare una nuova prenotazione
        public async Task CreateBookingAsync(CreateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                LessonDate = DateTime.Parse(dto.LessonDate), // Converte la data da stringa a DateTime
                Notes = dto.Notes,
                Status = "Pending" // Stato iniziale della prenotazione
            };

            await _bookingRepository.CreateBookingAsync(booking, cancellationToken);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }
        // Metodo per leggere una singola prenotazione tramite ID
        public async Task<BookingDto?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null) return null; // Se la prenotazione non esiste, ritorna null

            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                LessonDate = booking.LessonDate.ToString("yyyy-MM-ddTHH:mm:ss"), // Formattazione della data
                Status = booking.Status,
                Notes = booking.Notes
            };
        }
        // Metodo per ottenere tutte le prenotazioni
        public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync(CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);
            return bookings.Select(booking => new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                LessonDate = booking.LessonDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                Status = booking.Status,
                Notes = booking.Notes
            });
        }
        // Metodo per aggiornare una prenotazione
        public async Task<BookingDto?> UpdateBookingAsync(UpdateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(dto.Id, cancellationToken);
            if (booking == null) return null;
            // Aggiornamento dei dati della prenotazione
            booking.LessonDate = DateTime.Parse(dto.LessonDate);
            booking.Status = dto.Status;
            booking.Notes = dto.Notes;

            _bookingRepository.UpdateBooking(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                LessonDate = booking.LessonDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                Status = booking.Status,
                Notes = booking.Notes
            };
        }
        // Metodo per eliminare una prenotazione tramite ID
        public async Task<bool> DeleteBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null) return false; // Se la prenotazione non esiste, ritorna false

            _bookingRepository.DeleteBooking(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
        // Metodo per assegnare un utente a una prenotazione
        public async Task<bool> UpdateUserForBookingAsync(int bookingId, int userId, CancellationToken cancellationToken = default)
        {
            // Verifica l'esistenza dell'utente tramite chiamata HTTP al UserService
            var user = await _clientHttp.GetUserByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return false;
            }

            // Recupera la prenotazione
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {bookingId} not found.");
                return false;
            }
            // Assegna l'utente alla prenotazione
            booking.UserId = userId;

            _bookingRepository.UpdateBooking(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
