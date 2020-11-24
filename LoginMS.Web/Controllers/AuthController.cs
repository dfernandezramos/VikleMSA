using System;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LoginMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
        }
        
        [HttpGet]
        public async Task<ActionResult<LoginData>> GetToken(string email, string password)
        {
            return Ok(new LoginData());
        }
        
        [HttpPut]
        public async Task<IActionResult> SignUp(string email, string password, string name, string surname, string phone)
        {
            return Ok();
        }
    }
}