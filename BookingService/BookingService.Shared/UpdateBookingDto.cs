namespace BookingService.Shared
{
    public class UpdateBookingDto
    {
        public int Id { get; set; } // ID della prenotazione da aggiornare
        public string LessonDate { get; set; } // Nuova data e ora della lezione in formato stringa
        public string Status { get; set; } // Nuovo stato della prenotazione (es. Pending, Confirmed, Canceled)
        public string Notes { get; set; } = string.Empty; // Nuove note opzionali
    }
}
