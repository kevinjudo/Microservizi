
using UserService.Business.Abstraction; // Importa l'interfaccia della logica di business
using UserService.Business; // Importa l'implementazione della logica di business
using UserService.Repository; // Importa il livello repository
using UserService.Repository.Abstraction; // Importa le interfacce dei repository
using Microsoft.EntityFrameworkCore; // Importa il supporto per Entity Framework Core (Database ORM)



var builder = WebApplication.CreateBuilder(args); // Crea il builder dell'applicazione

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Metto in ascolto sulla porta 5000 (5001 per BookingService)
});
// Configura il UserDbContext
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserDb")));


builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserBusiness, UserBusiness>();


builder.Services.AddControllers();

// Configura Swagger per la documentazione delle API
builder.Services.AddEndpointsApiExplorer(); // Aggiunge il supporto alle Minimal APIs, anche se noi utilizzeremo i controller
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

app.UseAuthorization(); 

app.MapControllers(); 
 
app.Run(); // Avvia l'applicazione
