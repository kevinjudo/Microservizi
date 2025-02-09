namespace BookingService.Repository.Model
{
    public class Booking
    {
        public int Id { get; set; } // Identificativo univoco della prenotazione
        public int UserId { get; set; } // ID dell'utente che ha prenotato
        public DateTime LessonDate { get; set; } // Data e ora della lezione
        public string Status { get; set; } = "Pending"; // Stato della prenotazione (Pending, Confirmed, Canceled)
        public string Notes { get; set; } = string.Empty; // Note opzionali
    }
}
