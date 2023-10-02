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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return NotFound();

            var response = await _loginService.GetUserData(Guid.Parse(userId));
            return Ok(response);
        }

        [HttpPut("user-data")]
        [Authorize]
        public async Task<IActionResult> UpdateUserData([FromBody] UserDTO userData)
        {
            await _loginService.UpdateUserData(userData);
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
        public async Task<IActionResult> Login([FromBody] Credentials credentials)
        {
            var (id, success) = await _loginService.CheckCredentials(credentials);
            if (!success)
                return Unauthorized();

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"] ?? ""));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new Claim[]
            {
                new Claim("sub", id.ToString())
            };

            var jwtDefinition = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(5),
                signingCredentials
                );

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtDefinition);

            var res = new Response<string>
            {
                Result = true,
                Body = new string[] { token },
                Notes = ""
            };

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            var response = await _loginService.RegisterUser(user);
            if (!response.Result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("change-password")]
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
