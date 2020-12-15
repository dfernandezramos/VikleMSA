// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;

namespace LoginMS.Data
{
    /// <summary>
    /// This class implements the auth controller not found exception.
    /// </summary>
    public class AuthNotFoundException : ArgumentException
    {
        public AuthNotFoundException(string message) : base(message)
        {

        }
    }
}