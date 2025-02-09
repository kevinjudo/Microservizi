
using UserService.Business.Abstraction; // Importa l'interfaccia della logica di business
using UserService.Business; // Importa l'implementazione della logica di business
using UserService.Repository; // Importa il livello repository
using UserService.Repository.Abstraction; // Importa le interfacce dei repository
//using UserService.ClientHttp; // Importa il client HTTP per le comunicazioni con altri servizi
//using UserService.ClientHttp.Abstraction; // Importa l'interfaccia del client HTTP
// using UserService.Kafka; // Importa la gestione di Kafka per la messaggistica
// using UserService.Kafka.Abstraction; // Importa l'interfaccia per Kafka
using Microsoft.EntityFrameworkCore; // Importa il supporto per Entity Framework Core (Database ORM)



var builder = WebApplication.CreateBuilder(args); // Crea il builder dell'applicazione

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); 
});

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
app.UseHttpsRedirection(); // -> Aggiunge il middleware per reindirizzare le richieste HTTP verso HTTPS

// Abilita l'autorizzazione (in futuro potrebbe includere autenticazione)
app.UseAuthorization(); // -> Aggiunge il middleware per le funzionalità di autorizzazione

app.MapControllers(); // Mappa i controller alle route HTTP
                     // Aggiunge gli endpoint per le action dei controller: permettono di utilizzare le
                    // funzionalità di routing necessarie a inoltrare le richieste alle action.
 
app.Run(); // Avvia l'applicazione
