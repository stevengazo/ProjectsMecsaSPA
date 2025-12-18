using Irony.Parsing;
using ProjectsMecsaSPA.DTO;
using static System.Net.WebRequestMethods;

namespace ProjectsMecsaSPA.Services
{
    public class APICarServices
    {
     
        private readonly HttpClient _httpClient;
        public APICarServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Car>>("Cars");
        }
    }
}
