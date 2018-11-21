using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FoodTracker.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FoodTracker.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        public TokenController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginDto login)
        {
            IActionResult response = Unauthorized();
            var user = authenticate(login);

            if (user == null)
                return response;
            var tokenString = buildToken(user);
            response = Ok(new { token = tokenString });

            return response;
        }

        private UserDto authenticate(LoginDto login)
        {
            UserDto user = null;

            if (login.Username == "testUser" && login.Password == "testPassword")
            {
                user = new UserDto { Name = "Simple Test", Email = "stest@domain.com" };
            }
            return user;
        }

        private string buildToken(UserDto user)
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
