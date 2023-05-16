using System.Globalization;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<ServiceResponse<string>> GetCurrentWeather(string location);
        Task<ServiceResponse<string>> GetWeatherForecast(string location, int days);
        Task<ServiceResponse<string>> GetWeatherHistory(string location, int days);
    }
}
