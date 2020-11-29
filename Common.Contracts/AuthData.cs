using System;

namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the login full data stored in the API
    /// </summary>
    public class AuthData
    {
        /// <summary>
        /// Gets or sets the AuthData identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user UserId.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string Password { get; set; }
    }
}