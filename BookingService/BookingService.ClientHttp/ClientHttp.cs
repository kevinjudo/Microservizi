using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BookingService.Shared;
using BookingService.ClientHttp.Abstraction;

namespace BookingService.ClientHttp
{
    public class ClientHttp : IClientHttp
    {
        private readonly HttpClient _httpClient;

        public ClientHttp(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"/api/User/ReadUser/{userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null;
        }
    }
}
