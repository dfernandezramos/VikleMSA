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