using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BookingService.Shared;
using BookingService.ClientHttp.Abstraction;

namespace BookingService.ClientHttp
{
// Implementazione della classe per la gestione delle chiamate HTTP al servizio utenti
    public class ClientHttp : IClientHttp
    {
        private readonly HttpClient _httpClient;
        // Costruttore che inietta HttpClient per effettuare le richieste HTTP
        public ClientHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Metodo per ottenere i dettagli di un utente tramite il suo ID
        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            // Esegue una richiesta HTTP GET per ottenere le informazioni dell'utente
            var response = await _httpClient.GetAsync($"/api/User/ReadUser/{userId}");
            if (response.IsSuccessStatusCode)
            {
                // Se la richiesta ha successo, deserializza la risposta JSON in un oggetto UserDto
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            // Restituisce null se la richiesta non è andata a buon fine
            return null;
        }
    }
}
