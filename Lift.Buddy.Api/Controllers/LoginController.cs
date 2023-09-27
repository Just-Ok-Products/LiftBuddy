using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lift.Buddy.API.Controllers
{
    //TODO: per convenzione si dovrebbero usare _ invece che - negli endpoint
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;

        public LoginController(IConfiguration configuration, ILoginService loginService)
        {
            _configuration = configuration;
            _loginService = loginService;
        }

        #region User Data
        [HttpGet("user-data")]
        [Authorize]
        public async Task<IActionResult> GetUserData()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            var response = await _loginService.GetUserData(username);
            return Ok(response);
        }

        //QUESTION: non dovrebbe tornare ok anche qeusta? cosa succede se le richieste falliscono?
        // (tipo se il db è momentaneamente irraggiungibile?). immagino che il backend esploda
        [HttpPut("user-data")]
        [Authorize]
        public async Task<IActionResult> UpdateUserData([FromBody] UserData userData)
        {
            var response = await _loginService.UpdateUserData(userData);
            return NoContent();
        }
        #endregion

        [HttpPost("security-questions")]
        public async Task<IActionResult> GetSecurityQuestions([FromBody] LoginCredentials loginCredentials)
        {
            var response = await _loginService.GetSecurityQuestions(loginCredentials.Username);
            return Ok(response);
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials)
        {
            if (loginCredentials == null || !_loginService.CheckCredentials(loginCredentials))
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"] ?? ""));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", loginCredentials.Username)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(5),
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            // qua userei un array e IEnumerable in Response.Body
            var tokens = new List<string>
            {
                tokenToReturn
            };

            var loginResp = new Response<string>
            {
                Result = true,
                Body = tokens,
                Notes = ""
            };
            return Ok(loginResp);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationCredentials registrationCredentials)
        {
            var response = await _loginService.RegisterUser(registrationCredentials);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] LoginCredentials loginCredentials)
        {
            var response = await _loginService.ChangePassword(loginCredentials);

            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
