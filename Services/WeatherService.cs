namespace WeatherAPI.Services
{
    public class WeatherService : IWeatherService
    {
        public Task<string> GetCurrentWeather(string location)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetWeatherForecast(string location)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetWeatherHistory(string location)
        {
            throw new NotImplementedException();
        }
    }
}
