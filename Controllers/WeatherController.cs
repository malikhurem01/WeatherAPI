using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
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

        [HttpGet("/current")]
        public async Task<ActionResult<ServiceResponse<string>>> GetCurrentWeather(string location)
        {
            return await _weatherService.GetCurrentWeather(location);
        }

        [HttpGet("/forecast")]
        public Task<ActionResult<string>> GetWeatherForecast(string location)
        {
            return null;
        }

        [HttpGet("/history")]
        public Task<ActionResult<string>> GetWeatherHistory(string location)
        {
            return null;
        }
    }
}
