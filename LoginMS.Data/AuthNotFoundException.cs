using System;

namespace LoginMS.Data
{
    public class AuthNotFoundException : ArgumentException
    {
        public AuthNotFoundException(string message) : base(message)
        {

        }
    }
}