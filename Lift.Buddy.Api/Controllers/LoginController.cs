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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;

        public AuthController(IConfiguration configuration, ILoginService loginService)
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

        [HttpPut("user-data")]
        [Authorize]
        public async Task<IActionResult> UpdateUserData([FromBody] UserDTO userData)
        {
            var response = await _loginService.UpdateUserData(userData);
            return NoContent();
        }
        #endregion

        [HttpPost("security-questions")]
        [Authorize]
        public async Task<IActionResult> GetSecurityQuestions([FromBody] Credentials credentials)
        {
            var response = await _loginService.GetSecurityQuestions(credentials.Username);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Credentials loginCredentials)
        {
            var success = await _loginService.CheckCredentials(loginCredentials);
            if (!success)
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

            var tokens = new List<string>
            {
                tokenToReturn
            };

            var res = new Response<string>
            {
                Result = true,
                Body = tokens,
                Notes = ""
            };

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO registrationCredentials)
        {
            var response = await _loginService.RegisterUser(registrationCredentials);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] Credentials loginCredentials)
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
