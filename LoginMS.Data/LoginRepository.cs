// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace LoginMS.Data
{
    /// <summary>
    /// This class contains the implementation of the login repository
    /// </summary>
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        IMongoCollection<AuthData> AuthData => MongoDatabase.GetCollection<AuthData>("AuthData");

        public LoginRepository(MongoDbSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// This method gets the authorization data for the given email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The authorization data</returns>
        public async Task<AuthData> GetAuthDataByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(AuthData.Find(c => c.Email == email.ToLower(), new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }
        
        /// <summary>
        /// This method gets the authorization data for the given email.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The authorization data</returns>
        public async Task<AuthData> GetAuthDataById(string userId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(AuthData.Find(c => c.UserId == userId, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }

        /// <summary>
        /// This method saves in the database the provided user authorization data.
        /// </summary>
        /// <param name="authData">The user authorization data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task NewAuthData(AuthData authData, CancellationToken cancellationToken = default)
        {
            await AuthData.InsertOneAsync(authData, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        /// <summary>
        /// This method updates the authorization data for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task UpdateAuthData(string userId, string email, string password, CancellationToken cancellationToken = default)
        {
            var authData = await GetAuthDataById(userId, cancellationToken);
            if (authData == default(AuthData))
            {
                throw new AuthNotFoundException($"Failed to find auth data with user id {userId}");
            }
            authData.Email = email.ToLower();
            authData.Password = password;
            await AuthData.ReplaceOneAsync(c => c.UserId == userId, authData, new ReplaceOptions { IsUpsert = false }, cancellationToken);
        }
        
        /// <summary>
        /// This method resets the user password for the given user email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task ResetPassword(string email, CancellationToken cancellationToken = default)
        {
            var result = await GetAuthDataByEmail(email, cancellationToken);
            
            if (result != null)
            {
                await UpdateAuthData(result.UserId, result.Email.ToLower(), "resetPassword", cancellationToken);
            }
        }
    }
}