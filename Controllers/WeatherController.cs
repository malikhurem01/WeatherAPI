using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService) {
            _weatherService = weatherService;
        }

        [HttpGet]
        public Task<ActionResult<string>> GetCurrentWeather(string location)
        {
            return null;
        }

        [HttpGet]
        public Task<ActionResult<string>> GetWeatherForecast(string location)
        {
            return null;
        }

        [HttpGet]
        public Task<ActionResult<string>> GetWeatherHistory(string location)
        {
            return null;
        }

        /*
         /weather/current: returns the current weather conditions for a specific location
/weather/forecast: returns the weather forecast for a specific location
/weather/history: returns historical weather data for a specific location
         */
    }
}
