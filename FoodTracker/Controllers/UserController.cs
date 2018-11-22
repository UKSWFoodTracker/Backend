using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using FoodTracker.Domain.Helpers.Exceptions;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto login)
        {
            if (!ModelState.IsValid)
                throw new Exception("Login model is invalid");

            Account user;
            try
            {
                user = await _userService.AuthenticateAsync(login.Username, login.Password);
            }
            catch (Exception exception)
            {
                if (exception is AccountNotFoundException || exception is InvalidPasswordException)
                {
                    return Unauthorized();
                }
                throw;
            }
                
            var token = buildToken(user);
            return Ok(new {token });
        }

        private string buildToken(Account user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}