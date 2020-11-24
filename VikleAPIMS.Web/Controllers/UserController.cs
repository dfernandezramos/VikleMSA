using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace VikleAPIMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }
        
        [HttpGet]
        public async Task<ActionResult<User>> GetUserFromId(string userId)
        {
            return Ok(new User());
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateUserData(string userId, string email, string password, string name, string surname, string phone)
        {
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            return Ok();
        }
        
        [HttpGet]
        [Route ("vehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetUserVehicles(string userId)
        {
            return Ok(new List<Vehicle>());
        }
        
        [HttpDelete]
        [Route ("vehicles")]
        public async Task<IActionResult> DeleteUserVehicle(string userId)
        {
            return Ok();
        }
        
        [HttpPost]
        [Route ("vehicles")]
        public async Task<IActionResult> UpdateUserVehicle(string userId)
        {
            return Ok();
        }
        
        [HttpGet]
        [Route ("reparations/current")]
        public async Task<ActionResult<Reparation>> GetUserCurrentReparation(string userId)
        {
            return Ok(new Reparation());
        }
        
        [HttpGet]
        [Route ("dates")]
        public async Task<ActionResult<IEnumerable<Date>>> GetUserDates(string userId)
        {
            return Ok(new List<Date>());
        }
        
        [HttpPost]
        [Route ("dates")]
        public async Task<IActionResult> UpdateUserDate(string reparationDate, string plateNumber, string reason, string idClient, string status)
        {
            return Ok(new List<Date>());
        }
    }
}