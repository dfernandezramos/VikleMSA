// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the signup data the user fills in the signup page.
    /// </summary>
    public class SignupData
    {
        /// <summary>
        /// Gets or sets the user name
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the user surname
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Gets or sets the user phone
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the user email
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets the user password
        /// </summary>
        public string Password { get; set; }
    }
}