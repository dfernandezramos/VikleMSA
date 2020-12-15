// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System.IO.MemoryMappedFiles;
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
        Task<AuthData> GetAuthDataByEmail(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method saves in the database the provided user authorization data.
        /// </summary>
        /// <param name="authData">The user authorization data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewAuthData(AuthData authData, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method updates the authorization data for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateAuthData(string userId, string email, string password, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// This method resets the user password for the given user email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task ResetPassword(string email, CancellationToken cancellationToken = default);
    }
}