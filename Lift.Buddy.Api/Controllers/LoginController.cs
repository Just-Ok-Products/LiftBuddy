﻿using Lift.Buddy.API.Interfaces;
using Lift.Buddy.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lift.Buddy.API.Controllers
{
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

        [HttpGet("security-questions")]
        public IActionResult GetSecurityQuestions()
        {

        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials loginCredentials) {
            if (loginCredentials == null || !_loginService.CheckCredentials(loginCredentials))
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"] ?? ""));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", loginCredentials.Username!));

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
            var loginResp = new Response
            {
                result = true,
                body = tokenToReturn,
                notes = ""
            };
            return Ok(loginResp);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationCredentials registrationCredentials) {
            var response = await _loginService.RegisterUser(registrationCredentials);
            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] LoginCredentials loginCredentials)
        {
            var response = await _loginService.ChangePassword(loginCredentials);

            if (!response.result)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
