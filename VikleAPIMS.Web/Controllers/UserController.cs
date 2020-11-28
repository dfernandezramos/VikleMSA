using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VikleAPIMS.Web.Controllers
{
    /// <summary>
    /// This class contains the controller for the user endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }
        
        /// <summary>
        /// This method gets the user with the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user.</returns>
        [HttpGet]
        public async Task<ActionResult<User>> GetUserFromId(string userId)
        {
            return Ok(new User());
        }
        
        /// <summary>
        /// This method updates the provided data for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="email">The user email</param>
        /// <param name="password">The user password</param>
        /// <param name="name">The user name</param>
        /// <param name="surname">The user surname</param>
        /// <param name="phone">The user phone</param>
        [HttpPost]
        public async Task<IActionResult> UpdateUserData(string userId, string email, string password, string name, string surname, string phone)
        {
            return Ok();
        }
        
        /// <summary>
        /// This method updates the user password for the user with the provided email
        /// </summary>
        /// <param name="email">The user email</param>
        [HttpPost]
        public async Task<IActionResult> RecoverPassword(string email)
        {
            return Ok();
        }
        
        /// <summary>
        /// This method gets the user vehicles of the user with the provided identifier
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>A list with the user vehicles</returns>
        [HttpGet]
        [Route ("vehicles")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetUserVehicles(string userId)
        {
            return Ok(new List<Vehicle>());
        }
        
        /// <summary>
        /// This method deletes the provided vehicle for the provided user 
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The vehicle plate number</param>
        [HttpDelete]
        [Route ("vehicles")]
        public async Task<IActionResult> DeleteUserVehicle(string userId, string plateNumber)
        {
            return Ok();
        }
        
        /// <summary>
        /// This method updates the provided vehicle data for the provided vehicle identifier
        /// </summary>
        /// <param name="oldPlateNumber">The current saved vehicle plate number in the API</param>
        /// <param name="newPlateNumber">The new vehicle plate number to be saved in the API</param>
        /// <param name="model">The vehicle number</param>
        /// <param name="vehicleType">The vehicle type</param>
        /// <param name="year">The vehicle year</param>
        /// <param name="lastITV">The vehicle last ITV date</param>
        /// <param name="lastTBDS">The vehicle last TBDS date</param>
        /// <param name="IdClient">The client identifier</param>
        [HttpPost]
        [Route ("vehicles")]
        public async Task<IActionResult> UpdateUserVehicle(string oldPlateNumber, string newPlateNumber, string model,
            string vehicleType, string year, string lastITV, string lastTBDS, string IdClient)
        {
            return Ok();
        }
        
        /// <summary>
        /// This method gets the current reparation for the provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns>The current reparation of the vehicle</returns>
        [HttpGet]
        [Route ("reparations/current")]
        public async Task<ActionResult<Reparation>> GetUserCurrentReparation(string plateNumber)
        {
            return Ok(new Reparation());
        }
        
        /// <summary>
        /// This method gets the dates of the provided user identifier
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>A list with the user dates</returns>
        [HttpGet]
        [Route ("dates")]
        public async Task<ActionResult<IEnumerable<Date>>> GetUserDates(string userId)
        {
            return Ok(new List<Date>());
        }
        
        /// <summary>
        /// This method saves the provided date data in the API
        /// </summary>
        /// <param name="reparationDate">The reparation date</param>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="reason">The reparation reason</param>
        /// <param name="idClient">The client identifier</param>
        /// <param name="status">The reparation status</param>
        /// <returns></returns>
        [HttpPost]
        [Route ("dates")]
        public async Task<IActionResult> UpdateUserDate(string reparationDate, string plateNumber, string reason, string idClient, string status)
        {
            return Ok(new List<Date>());
        }
    }
}