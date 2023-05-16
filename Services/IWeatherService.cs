using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<ServiceResponse<string>> GetCurrentWeather(string location);
        Task<ServiceResponse<string>> GetWeatherForecast(string location);
        Task<ServiceResponse<string>> GetWeatherHistory(string location);
    }
}
