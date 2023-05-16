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
                    serviceResponse.Message = "Successfully fetched data for " + location + ".";
                    serviceResponse.Data = responseBody;
                }
                catch (HttpRequestException ex)
                {
                    serviceResponse.Message = "Error: " + ex.Message;
                }
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> GetWeatherForecast(string location, int days)
        {
            var serviceResponse = new ServiceResponse<string>();
            string responseBody = string.Empty;
            var URL = "http://api.weatherapi.com/v1/forecast.json?key=" + API_KEY + "&q=" + location + "&days=" + days + "&aqi=no&alerts=no";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    serviceResponse.Message = "Successfully fetched data for " + location + ".";
                    serviceResponse.Data = responseBody;
                }
                catch (HttpRequestException ex)
                {
                    serviceResponse.Message = "Error: " + ex.Message;
                }
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> GetWeatherHistory(string location, int days)
        {
            if(days > 3)
            {
                throw new Exception("ERROR: You can only search for up to 3 days in history.");
            }
            var serviceResponse = new ServiceResponse<string>();
            string responseBody = string.Empty;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddDays(-days);
            var URL = "http://api.weatherapi.com/v1/history.json?key=" + API_KEY + "&q=" + location + "&dt=" + dateTime;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);
                    response.EnsureSuccessStatusCode();
                    responseBody = await response.Content.ReadAsStringAsync();
                    serviceResponse.Message = "Successfully fetched data for " + location + ".";
                    serviceResponse.Data = responseBody;
                }
                catch (HttpRequestException ex)
                {
                    serviceResponse.Message = "Error: " + ex.Message;
                }
            }
            return serviceResponse;
        }
    }
}
