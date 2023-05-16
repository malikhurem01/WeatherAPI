using System.Runtime.CompilerServices;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {

        private readonly string API_KEY = "2924eefd27ce4bf1807123158231605";
        public WeatherService()
        {
        }

        public async Task<ServiceResponse<string>> GetCurrentWeather(string location)
        {
            var serviceResponse = new ServiceResponse<string>();
            string responseBody = string.Empty;
            var URL = "http://api.weatherapi.com/v1/current.json?key=" + API_KEY + "&q=" + location + "&aqi=no";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();

                    responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            serviceResponse.Message = "Successfully fetched data for " + location + ".";
            serviceResponse.Data = responseBody;
            return serviceResponse;
        }

        Task<ServiceResponse<string>> IWeatherService.GetWeatherForecast(string location)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<string>> IWeatherService.GetWeatherHistory(string location)
        {
            throw new NotImplementedException();
        }
    }
}
