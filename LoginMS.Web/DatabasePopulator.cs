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

            var clientEmail = "client@email.com";
            var client = await _repository.GetAuthDataByEmail(clientEmail);

            if (client == null)
            {
                var clientId = "28feef62-08c1-4b14-9ea4-13e007d1f002";
                await _repository.NewAuthData(new AuthData
                {
                    UserId = clientId,
                    Email = clientEmail,
                    Password = "Client123",
                    Token = GenerateToken(clientEmail, UserRole.Client, clientId)
                }, default);
            }
            
            var workerEmail = "worker@email.com";
            var worker = await _repository.GetAuthDataByEmail(workerEmail);
            
            if (worker == null)
            {
                var workerId = "0c0910e9-0ab2-4d70-9ea2-21198dfb36ac";
                await _repository.NewAuthData(new AuthData
                {
                    UserId = workerId,
                    Email = "worker@email.com",
                    Password = "Worker123",
                    Token = GenerateToken(workerEmail, UserRole.Worker, workerId)
                }, default);
            }
            
            var adminEmail = "admin@email.com";
            var admin = await _repository.GetAuthDataByEmail(adminEmail);
            
            if (admin == null)
            {
                var adminId = "115deaa7-5405-4db4-8ee8-cc8162d67bbb";
                await _repository.NewAuthData(new AuthData
                {
                    UserId = adminId,
                    Email = "admin@email.com",
                    Password = "Admin123",
                    Token = GenerateToken(adminEmail, UserRole.Admin, adminId)
                }, default);
            }
            
            _log.Info("Login database population success");
        }
        
        string GenerateToken(string userName, string userRole, string id)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, id),
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