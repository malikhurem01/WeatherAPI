using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DTOs;
using WeatherAPI.Models;
using WeatherAPI.Services;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("/login")]
        public ActionResult<ServiceResponse<LoginResponse>> LoginUser(UserLoginDto userLoginDto)
        {
            var response = _authService.Login(userLoginDto);
            return Ok(response);
        }

        [HttpPost("/signup")]
        public async Task<ActionResult<ServiceResponse<LoginResponse>>> SignUpUser(UserSignUpDto userSignUpDto)
        {
            var response = await _authService.SignUp(userSignUpDto);
            return Ok(response);
        }
    }
}
