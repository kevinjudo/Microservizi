using BookingService.Business.Abstraction; // Importa l'interfaccia della logica di business
using BookingService.Business; // Importa l'implementazione della logica di business
using BookingService.Repository; // Importa il livello repository
using BookingService.Repository.Abstraction; // Importa le interfacce dei repository
using Microsoft.EntityFrameworkCore; // Importa il supporto per Entity Framework Core (Database ORM)
using BookingService.ClientHttp;
using BookingService.ClientHttp.Abstraction;


var builder = WebApplication.CreateBuilder(args); // Crea il builder dell'applicazione

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // Cambia la porta in 5001 per evitare conflitti con UserService
});

// Configura il DbContext per BookingService
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDb")));

// Registra i repository e la logica di business
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingBusiness, BookingBusiness>();

builder.Services.AddHttpClient<IClientHttp, ClientHttp>(client =>
{
    client.BaseAddress = new Uri("http://userservice:5000"); // URL del UserService
});

// Aggiunge il supporto ai controller
builder.Services.AddControllers();

// Configura Swagger per la documentazione delle API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); // Costruisce l'applicazione

// Configura Swagger solo se l'app è in modalità sviluppo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Abilita la redirezione HTTPS per garantire connessioni sicure
app.UseHttpsRedirection();

// Abilita l'autorizzazione (in futuro potrebbe includere autenticazione)
app.UseAuthorization();

app.MapControllers(); // Mappa i controller alle route HTTP

app.Run(); // Avvia l'applicazione
