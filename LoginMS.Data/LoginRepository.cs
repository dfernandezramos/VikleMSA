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
            return await Task.FromResult(AuthData.Find(c => c.Email == email, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
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
            var authData = await GetAuthDataByEmail(userId, cancellationToken);
            if (authData == default(AuthData))
            {
                throw new AuthNotFoundException($"Failed to find auth data with ID {userId}");
            }
            authData.Email = email;
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
                await UpdateAuthData(result.UserId, result.Email, "resetPassword", cancellationToken);
            }
        }
    }
}