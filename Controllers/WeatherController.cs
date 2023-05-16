using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<ServiceResponse<string>>> GetWeatherForecast(string location, int days)
        {
            return await _weatherService.GetWeatherForecast(location, days);
        }

        [HttpGet("/history")]
        public async Task<ActionResult<ServiceResponse<string>>> GetWeatherHistory(string location, int days)
        {
            return await _weatherService.GetWeatherHistory(location, days);
        }
    }
}
