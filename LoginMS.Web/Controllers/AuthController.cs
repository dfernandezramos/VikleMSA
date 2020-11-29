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
using Common.Domain;
using LoginMS.Data;
using Microsoft.AspNetCore.Http;

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
        private readonly ILoginRepository _repository;
        private readonly ILog _log;

        public AuthController(IConfiguration configuration, ILoginRepository repository, ILog log)
        {
            _configuration = configuration;
            _repository = repository;
            _log = log;
        }
        
        /// <summary>
        /// This method gets the token for the provided login credentials
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>The user login data</returns>
        [HttpGet]
        [ProducesResponseType(typeof(LoginData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginData>> GetToken(string email, string password)
        {
            _log.Info("Calling get token endpoint...");

            var authData = await _repository.GetAuthDataByEmail(email);
            if (authData == null || authData.Password != password)
            {
                _log.Error("User with the provided credentials not found");
                return Forbid();
            }

            return Ok(new LoginData
            {
                Token = authData.Token,
                UserId = authData.UserId
            });
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp(string email, string password, string name, string surname, string phone)
        {
            _log.Info("Calling signup endpoint...");

            var authData = await _repository.GetAuthDataByEmail(email);
            if (authData != null)
            {
                _log.Error("User with the provided email already exists");
                return Conflict();
            }

            var userId = Guid.NewGuid().ToString();
            var token = GenerateToken(name, UserRole.Client.ToString(), userId);
            
            await _repository.NewAuthData(new AuthData
            {
                UserId = userId,
                Email = email,
                Password = password,
                Token = token
            });

            return Ok();
        }

        string GenerateToken(string userName, string userRole, string userId)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, userId),
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