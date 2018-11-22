using System;
using System.Threading.Tasks;
using FoodTracker.Domain.Helpers.Exceptions;
using FoodTracker.Domain.Services.Interfaces;
using FoodTracker.DTO;
using FoodTracker.Helpers;
using FoodTracker.Helpers.Exceptions;
using FoodTracker.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public UsersController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto login)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelException();

            Account user;
            try
            {
                user = await _userService.AuthenticateAsync(login.Username, login.Password);
            }
            catch (Exception exception)
            {
                if (exception is AccountNotFoundException || exception is PasswordInvalidException)
                {
                    return Unauthorized();
                }
                throw;
            }

            var token = Jwt.BuildToken(user, _config);
            return Ok(new { token });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto register)
        {
            if (!ModelState.IsValid)
                throw new InvalidModelException();

            var user = new Account { Email = register.Email, UserName = register.Name };
            await _userService.CreateAsync(user, register.Password);

            try
            {
                user = await _userService.AuthenticateAsync(register.Name, register.Password);
            }
            catch (Exception exception)
            {
                if (exception is AccountNotFoundException || exception is PasswordInvalidException)
                {
                    return Unauthorized();
                }
                throw;
            }

            var token = Jwt.BuildToken(user, _config);
            return Created(nameof(LoginAsync), new { token });
        }
    }
}