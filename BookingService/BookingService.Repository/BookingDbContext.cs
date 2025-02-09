using Microsoft.EntityFrameworkCore;
using BookingService.Repository.Model;

namespace BookingService.Repository
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; } // Tabella delle prenotazioni

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurazione della tabella Booking
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id); // Imposta Id come chiave primaria

            modelBuilder.Entity<Booking>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd(); // Auto-incremento per l'Id

            modelBuilder.Entity<Booking>()
                .Property(b => b.UserId)
                .IsRequired(); // Relazione obbligatoria con l'utente

            modelBuilder.Entity<Booking>()
                .Property(b => b.LessonDate)
                .IsRequired(); // Data obbligatoria

            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(50); // Stato con lunghezza massima

            modelBuilder.Entity<Booking>()
                .Property(b => b.Notes)
                .HasMaxLength(500); // Note opzionali con limite massimo
        }
    }
}
