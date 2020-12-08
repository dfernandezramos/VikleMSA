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
using Common.Contracts.Events;
using Common.Domain;
using LoginMS.Data;
using MessageBroker.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IProducer _mbProducer;

        public AuthController(IConfiguration configuration, ILoginRepository repository, ILog log, IProducer mbProducer)
        {
            _configuration = configuration;
            _repository = repository;
            _log = log;
            _mbProducer = mbProducer;
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

            var authData = await _repository.GetAuthDataByEmail(email.ToLower());
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
        /// This method resets the password for the provided user email
        /// </summary>
        /// <param name="email">The user email</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody]string email)
        {
            _log.Info("Calling reset password endpoint...");

            await _repository.ResetPassword(email.ToLower());
            return Ok();
        }
        
        /// <summary>
        /// This method puts the provided user signup data in the login database.
        /// </summary>
        /// <param name="data">The signup data</param>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromBody]SignupData data)
        {
            _log.Info("Calling signup endpoint...");

            return await RegisterUser(data, UserRole.Client);
        }
        
        /// <summary>
        /// This method puts the provided user signup data in the login database.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="data">The signup data</param>
        [HttpPut]
        [Route("worker")]
        [Authorize (Roles = UserRole.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterWorker(string workshopId, [FromBody]SignupData data)
        {
            _log.Info("Calling signup worker endpoint...");
            
            return await RegisterUser(data, UserRole.Worker, workshopId);
        }

        async Task<IActionResult> RegisterUser(SignupData data, string role, string idWorkshop = null)
        {
            var authData = await _repository.GetAuthDataByEmail(data.Email.ToLower());
            if (authData != null)
            {
                _log.Error("User with the provided email already exists");
                return Conflict();
            }

            var userId = Guid.NewGuid().ToString();
            var token = GenerateToken(data.Email.ToLower(), role, userId);
            
            await _repository.NewAuthData(new AuthData
            {
                UserId = userId,
                Email = data.Email.ToLower(),
                Password = data.Password,
                Token = token
            });
            
            // Notify the kafka message broker that a new user has been registered
            var userRegisteredEvent = new UserRegisteredEvent {
                User = new User
                {
                    Id = userId,
                    Name = data.Name,
                    Surname = data.Surname,
                    Email = data.Email.ToLower(),
                    Phone = data.Phone,
                    IsWorker = role == UserRole.Worker,
                    IdWorkshop = idWorkshop
                }
            };
            await _mbProducer.Send (userRegisteredEvent, "UserRegistration");

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
                expires: DateTime.Now.AddYears(500),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}