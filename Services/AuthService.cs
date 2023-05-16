using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeatherAPI.DTOs;
using WeatherAPI.InMemoryData;
using WeatherAPI.Models;

namespace WeatherAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Username),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("Authentication:SecurityKey").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(_configuration.GetSection("Authentication:ExpiryInMinutes").Value)),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public ServiceResponse<LoginResponse> Login(UserLoginDto user)
        {
            var serviceResponse = new ServiceResponse<LoginResponse>();
            LoginResponse loginResponse = new LoginResponse();
            try
            {
                using (var _context = new DataContext())
                {
                    User retrievedUser = _context.Users.First(users => users.Email.Equals(user.Email));
                    if (retrievedUser == null)
                    {
                        throw new Exception("User with email " + user.Email + " does not exist");
                    }
                    if (!retrievedUser.Password.Equals(user.Password))
                    {
                        throw new Exception("LOGIN ERROR: Wrong credentials.");
                    }
                    loginResponse.Token = this.CreateToken(retrievedUser);
                    loginResponse.Username = retrievedUser.Username;
                }
            }catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }
            serviceResponse.Data = loginResponse;
            serviceResponse.Message = "Authentication successful! Please use the token provided to you in the AUTHORIZATION header when sending requests to our API in the following format 'Bearer ${your token}'.";
            return serviceResponse;
        }

        public async Task<ServiceResponse<LoginResponse>> SignUp(UserSignUpDto userSignUp)
        {
            var serviceResponse = new ServiceResponse<LoginResponse>();
            LoginResponse loginResponse = new LoginResponse();
            try
            {
                using (var _context = new DataContext())
                {
                    User retrievedUser = _context.Users.FirstOrDefault(users => users.Email.Equals(userSignUp.Email));
                    if (retrievedUser == null)
                    {
                        User newUser = new User();
                        newUser.Email = userSignUp.Email;
                        newUser.Username = userSignUp.Email.Split('@').ElementAt(0);
                        newUser.Id = userSignUp.Email + newUser.Username;
                        newUser.Password = userSignUp.Password;
                        _context.Users.Add(newUser);
                        await _context.SaveChangesAsync();
                        loginResponse.Token = this.CreateToken(newUser);
                        loginResponse.Username = newUser.Username;
                    }

                    else
                    {
                        throw new Exception("ERROR: User already exists.");
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
            }
            serviceResponse.Data = loginResponse;
            serviceResponse.Message = "Sign up successful! User has been logged in. Please use the token provided to you in the AUTHORIZATION header when sending requests to our API in the following format 'Bearer ${your token}'.";
            return serviceResponse;
        }
    }
}
