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
        Task<AuthData> GetAuthDataByEmail(string email, CancellationToken cancellationToken);

        Task NewAuthData(AuthData authData, CancellationToken cancellationToken);

        Task UpdateAuthData(string userId, string email, string password, CancellationToken cancellationToken);
    }
}