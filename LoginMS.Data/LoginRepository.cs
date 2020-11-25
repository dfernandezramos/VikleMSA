using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace LoginMS.Data
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        IMongoCollection<AuthData> AuthData => MongoDatabase.GetCollection<AuthData>("AuthData");

        public LoginRepository(MongoDbSettings settings) : base(settings)
        {
        }

        public async Task<AuthData> GetAuthDataByEmail(string email, CancellationToken cancellationToken)
        {
            return await Task.FromResult(AuthData.Find(c => c.Email == email, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }

        public async Task NewAuthData(AuthData authData, CancellationToken cancellationToken)
        {
            await AuthData.InsertOneAsync(authData, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        public async Task UpdateAuthData(string userId, string email, string password, CancellationToken cancellationToken)
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
    }
}