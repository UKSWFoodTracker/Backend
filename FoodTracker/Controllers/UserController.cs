using System;
using System.Threading.Tasks;
using FoodTracker.Domain.Helpers.Exceptions;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Helpers;
using FoodTracker.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
                
            var token = Jwt.BuildToken(user, _config);
            return Ok(new {token });
        }
    }
}