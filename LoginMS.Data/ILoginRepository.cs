using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;

namespace LoginMS.Data
{
    /// <summary>
    /// This interface contains the definition of the login repository
    /// </summary>
    public interface ILoginRepository
    {
        /// <summary>
        /// This method gets the authorization data for the given email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The authorization data</returns>
        Task<AuthData> GetAuthDataByEmail(string email, CancellationToken cancellationToken);

        /// <summary>
        /// This method saves in the database the provided user authorization data.
        /// </summary>
        /// <param name="authData">The user authorization data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewAuthData(AuthData authData, CancellationToken cancellationToken);

        /// <summary>
        /// This method updates the authorization data for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateAuthData(string userId, string email, string password, CancellationToken cancellationToken);
    }
}