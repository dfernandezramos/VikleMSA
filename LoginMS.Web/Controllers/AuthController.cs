using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace LoginMS.Web.Controllers
{
    /// <summary>
    /// This class contains the auth controller for the API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;  
        
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        /// <summary>
        /// This method gets the token for the provided login credentials
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>The user login data</returns>
        [HttpGet]
        public async Task<ActionResult<LoginData>> GetToken(string email, string password)
        {
            return Ok(new LoginData());
        }
        
        /// <summary>
        /// This method puts the provided user signup data in the login database.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="name">The user name</param>
        /// <param name="surname">The user surname</param>
        /// <param name="phone">The user phone number</param>
        [HttpPut]
        public async Task<IActionResult> SignUp(string email, string password, string name, string surname, string phone)
        {
            var token = GenerateToken(name, UserRole.Client.ToString());
            
            return Ok();
        }

        string GenerateToken(string userName, string userRole)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRole)
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
  
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}