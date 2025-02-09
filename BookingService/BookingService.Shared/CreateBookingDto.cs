namespace BookingService.Shared
{
    public class CreateBookingDto
    {
        public int UserId { get; set; } // ID dell'utente che effettua la prenotazione
        public string LessonDate { get; set; } // Data e ora della lezione in formato stringa (es. "2025-02-10T10:00:00")
        public string Notes { get; set; } = string.Empty; // Note opzionali
    }
}
