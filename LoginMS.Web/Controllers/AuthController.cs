using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LoginMS.Web.Controllers
{
    /// <summary>
    /// This class contains the auth controller for the API
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
        }
        
        /// <summary>
        /// This method gets the token for the provided login credentials
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <returns>The user login data</returns>
        [HttpGet]
        public async Task<ActionResult<LoginData>> GetToken(string email, string password)
        {
            return Ok(new LoginData());
        }
        
        /// <summary>
        /// This method puts the provided user signup data in the login database.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="name">The user name</param>
        /// <param name="surname">The user surname</param>
        /// <param name="phone">The user phone number</param>
        [HttpPut]
        public async Task<IActionResult> SignUp(string email, string password, string name, string surname, string phone)
        {
            return Ok();
        }
    }
}