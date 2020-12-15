// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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