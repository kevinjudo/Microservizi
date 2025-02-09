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
        private readonly IClientHttp _clientHttp; // Aggiungi l'interfaccia
        public BookingBusiness(IBookingRepository bookingRepository, IClientHttp clientHttp, ILogger<BookingBusiness> logger)
        {
            _bookingRepository = bookingRepository;
            _clientHttp = clientHttp;
            _logger = logger;

        }

        public async Task CreateBookingAsync(CreateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                LessonDate = DateTime.Parse(dto.LessonDate),
                Notes = dto.Notes,
                Status = "Pending" // Stato predefinito
            };

            await _bookingRepository.CreateBookingAsync(booking, cancellationToken);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task<BookingDto?> ReadBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null) return null;

            return new BookingDto
            {
                Id = booking.Id,
                UserId = booking.UserId,
                LessonDate = booking.LessonDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                Status = booking.Status,
                Notes = booking.Notes
            };
        }

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

        public async Task<BookingDto?> UpdateBookingAsync(UpdateBookingDto dto, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(dto.Id, cancellationToken);
            if (booking == null) return null;

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

        public async Task<bool> DeleteBookingAsync(int bookingId, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null) return false;

            _bookingRepository.DeleteBooking(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateUserForBookingAsync(int bookingId, int userId, CancellationToken cancellationToken = default)
        {
            // Verifica l'esistenza dell'utente tramite ClientHttp
            var user = await _clientHttp.GetUserByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return false;
            }

            // Aggiorna il booking
            var booking = await _bookingRepository.ReadBookingAsync(bookingId, cancellationToken);
            if (booking == null)
            {
                _logger.LogWarning($"Booking with ID {bookingId} not found.");
                return false;
            }

            booking.UserId = userId;

            _bookingRepository.UpdateBooking(booking);
            await _bookingRepository.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
