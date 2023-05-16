namespace WeatherAPI.Services
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeather(string location);
        Task<string> GetWeatherForecast(string location);
        Task<string> GetWeatherHistory(string location);
    }
}
