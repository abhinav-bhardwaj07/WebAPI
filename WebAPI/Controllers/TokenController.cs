using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/token")]
    public class TokenController : Controller
    {
        private IConfiguration _configuration;
        private string _email = "Test@123.com", _password = "Test@12345";
        public TokenController(IConfiguration configuration)
        {
             _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Email, string Password)
        {
            if(string.Equals(Email, _email) && string.Equals(Password, _password))
            {
                var claims = new[]
                {
                    new Claim("Email",Email),
                    new Claim ("Password",Password),
                    new Claim  ("DateTime", DateTime.Now.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ThisIsMyTokenKey"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                   issuer: _configuration["Issuer"],
                   audience:  _configuration["Audience"],
                   claims: claims,
                   expires:DateTime.Now.AddMinutes(5),
                   signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                return BadRequest("Invalid Credentials");
            }
        }
    }
}
