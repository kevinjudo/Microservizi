using BookingService.Business.Abstraction;
using BookingService.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingBusiness _bookingBusiness;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingBusiness bookingBusiness, ILogger<BookingController> logger)
        {
            _bookingBusiness = bookingBusiness;
            _logger = logger;
        }

        [HttpPost(Name = "CreateBooking")]
        public async Task<ActionResult> CreateBooking([FromBody] CreateBookingDto dto)
        {
            await _bookingBusiness.CreateBookingAsync(dto);
            return Ok("Booking created successfully");
        }

        [HttpGet("{bookingId}", Name = "ReadBooking")]
        public async Task<ActionResult<BookingDto>> ReadBooking(int bookingId)
        {
            var booking = await _bookingBusiness.ReadBookingAsync(bookingId);
            return booking is not null ? Ok(booking) : NotFound("Booking not found");
        }

        [HttpGet("all", Name = "GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await _bookingBusiness.GetAllBookingsAsync();
            return bookings is not null ? Ok(bookings) : NotFound("No bookings found");
        }

        [HttpPut(Name = "UpdateBooking")]
        public async Task<ActionResult<BookingDto>> UpdateBooking([FromBody] UpdateBookingDto dto)
        {
            var updatedBooking = await _bookingBusiness.UpdateBookingAsync(dto);
            return updatedBooking is not null ? Ok(updatedBooking) : NotFound("Booking not found");
        }

        [HttpDelete("{bookingId}", Name = "DeleteBooking")]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            var deleted = await _bookingBusiness.DeleteBookingAsync(bookingId);
            return deleted ? Ok("Booking deleted successfully") : NotFound("Booking not found");
        }

        [HttpPut("assign-user", Name = "AssignUserToBooking")]
        public async Task<ActionResult> AssignUserToBooking([FromBody] AssignUserDto dto)
        {
            var success = await _bookingBusiness.UpdateUserForBookingAsync(dto.Id, dto.UserId);
            return success ? Ok("User assigned successfully") : NotFound("Booking not found");
        }


    }
}
