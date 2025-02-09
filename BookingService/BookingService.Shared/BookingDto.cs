namespace BookingService.Shared
{
    public class BookingDto
    {
        public int Id { get; set; } // ID della prenotazione
        public int UserId { get; set; } // ID dell'utente che ha effettuato la prenotazione
        public string LessonDate { get; set; } // Data e ora della lezione in formato stringa
        public string Status { get; set; } // Stato della prenotazione (es. Pending, Confirmed, Canceled)
        public string Notes { get; set; } // Note opzionali
    }
}
