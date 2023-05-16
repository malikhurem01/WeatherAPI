using WeatherAPI.DTOs;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<LoginResponse>> SignUp(UserSignUpDto userSignUp);
        ServiceResponse<LoginResponse> Login(UserLoginDto userlogin);
        string CreateToken(User user);
    }
}
