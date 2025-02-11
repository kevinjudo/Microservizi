using BookingService.Business.Abstraction; // Importa l'interfaccia della business
using BookingService.Business; // Importa l'implementazione della business
using BookingService.Repository; // Importa il livello repository
using BookingService.Repository.Abstraction; // Importa le interfacce del repository
using Microsoft.EntityFrameworkCore; // Importa il supporto per Entity Framework Core (DB ORM)
using BookingService.ClientHttp;
using BookingService.ClientHttp.Abstraction;


var builder = WebApplication.CreateBuilder(args); // Crea il builder dell'applicazione

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001); // metto in ascolto sulla porta 5001 (5000 per UserService)
});

// Configura il BookingDbContext
builder.Services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookingDb")));

// Registra i repository e la business
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingBusiness, BookingBusiness>();
//Configura la comunicazione ClientHttp
builder.Services.AddHttpClient<IClientHttp, ClientHttp>(client =>
{
    client.BaseAddress = new Uri("http://userservice:5000"); // URL per comunicare con UserService
});

// Aggiunge il supporto ai controller
builder.Services.AddControllers();

// Abilita l'API Explorer per individuare gli endpoint esposti nell'applicazione.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Abilita Swagger per generare la documentazione interattiva delle API.

var app = builder.Build(); // Costruisce l'applicazione

// Configura Swagger solo se l'app è in modalità sviluppo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Abilita la redirezione HTTPS per garantire connessioni sicure
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers(); 

app.Run(); // Avvio
