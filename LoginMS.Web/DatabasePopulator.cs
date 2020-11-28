using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Domain;
using LoginMS.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LoginMS.Web
{
    /// <summary>
    /// This class contains the database populator for the login microservice
    /// </summary>
    public class DatabasePopulator
    {
        private readonly ILoginRepository _repository;
        private readonly IConfiguration _configuration;
        private ILog _log;

        public DatabasePopulator(ILoginRepository repository, IConfiguration configuration, ILog log)
        {
            _repository = repository;
            _configuration = configuration;
            _log = log;
        }

        /// <summary>
        /// This method populates the database.
        /// </summary>
        public async Task Seed()
        {
            _log.Info("Populating Login database...");
            var clientId = Guid.NewGuid().ToString();
            var workerId = Guid.NewGuid().ToString();
            
            await _repository.NewAuthData(new AuthData
            {
                UserId = clientId,
                Email = "client@email.com",
                Password = "Client123",
                Token = GenerateToken("client@email.com", UserRole.Client.ToString(), clientId).Id
            }, default);
            await _repository.NewAuthData(new AuthData
            {
                UserId = workerId,
                Email = "worker@email.com",
                Password = "Worker123",
                Token = GenerateToken("worker@email.com", UserRole.Client.ToString(), workerId).Id
            }, default);
            
            _log.Info("Login database population success");
        }
        
        JwtSecurityToken GenerateToken(string userName, string userRole, string id)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, id),
                new Claim(ClaimTypes.Role, userRole)
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
  
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}