namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the login API call response data.
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        public string Token { get; set; }
    }
}