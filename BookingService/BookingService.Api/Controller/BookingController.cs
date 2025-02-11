using BookingService.Business.Abstraction;
using BookingService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingService.Api.Controllers
{
    [Route("api/[controller]/[action]")] // Definisce il percorso base dell'API
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBusiness _bookingBusiness;
        private readonly ILogger<BookingController> _logger;
        // Iniezione delle dipendenze per la logica di business e il logging
        public BookingController(IBookingBusiness bookingBusiness, ILogger<BookingController> logger)
        {
            _bookingBusiness = bookingBusiness;
            _logger = logger;
        }
        // Endpoint per creare una nuova prenotazione
        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            await _bookingBusiness.CreateBookingAsync(dto);
            return Ok("Booking created successfully");
        }
        // Endpoint per leggere una prenotazione specifica tramite ID
        [HttpGet("{bookingId}", Name = "ReadBooking")]
        public async Task<ActionResult<BookingDto>> ReadBooking(int bookingId)
        {
            var booking = await _bookingBusiness.ReadBookingAsync(bookingId);
            return booking is not null ? Ok(booking) : NotFound("Booking not found");
        }
        // Endpoint per ottenere tutte le prenotazioni
        [HttpGet("all", Name = "GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingBusiness.GetAllBookingsAsync();
            return bookings is not null ? Ok(bookings) : NotFound("No bookings found");
        }
        // Endpoint per modificare una prenotazione
        [HttpPut(Name = "UpdateBooking")]
        public async Task<ActionResult<BookingDto>> UpdateBooking([FromBody] UpdateBookingDto dto)
        {
            var updatedBooking = await _bookingBusiness.UpdateBookingAsync(dto);
            return updatedBooking is not null ? Ok(updatedBooking) : NotFound("Booking not found");
        }
        // Endpoint per eliminare una prenotazione tramite ID
        [HttpDelete("{bookingId}", Name = "DeleteBooking")]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            var deleted = await _bookingBusiness.DeleteBookingAsync(bookingId);
            return deleted ? Ok("Booking deleted successfully") : NotFound("Booking not found");
        }
        // Endpoint per assegnare un utente a una prenotazione
        [HttpPut("assign-user", Name = "AssignUserToBooking")]
        public async Task<ActionResult> AssignUserToBooking([FromBody] AssignUserDto dto)
        {
            var success = await _bookingBusiness.UpdateUserForBookingAsync(dto.Id, dto.UserId);
            return success ? Ok("User assigned successfully") : NotFound("Booking not found");
        }


    }
}
