using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VikleAPIMS.Data;

namespace VikleAPIMS.Web.Controllers
{
    /// <summary>
    /// This class contains the controller for the user endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILog _log;
        private readonly IVikleRepository _repository;
        
        public UserController(ILog log, IVikleRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        
        /// <summary>
        /// This method gets the user with the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user.</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(User),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetUserFromId(string userId)
        {
            _log.Info("Calling get user endpoint...");

            var user = await _repository.GetUserById(userId);

            if (user == null)
            {
                _log.Error("No user with the provided identifier exists.");
                return Forbid();
            }
            
            return Ok(user);
        }
        
        /// <summary>
        /// This method updates the provided data for the provided user identifier.
        /// </summary>
        /// <param name="user">The user data</param>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserData([FromBody]User user)
        {
            _log.Info("Calling update user endpoint...");

            try
            {
                await _repository.UpdateUser(user);
            }
            catch (ArgumentException)
            {
                _log.Error("No user with the provider id exists.");
                return Forbid();
            }

            return Ok();
        }
        
        /// <summary>
        /// This method gets the user vehicles of the user with the provided identifier
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>A list with the user vehicles</returns>
        [HttpGet]
        [Authorize]
        [Route ("vehicles")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Vehicle>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetUserVehicles(string userId)
        {
            _log.Info("Calling get user vehicles endpoint...");

            var vehicles = await _repository.GetUserVehicles(userId);
            return Ok(vehicles);
        }
        
        /// <summary>
        /// This method deletes the provided vehicle for the provided user 
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The vehicle plate number</param>
        [HttpDelete]
        [Authorize]
        [Route ("vehicles")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserVehicle(string userId, string plateNumber)
        {
            _log.Info("Calling delete user vehicle endpoint...");

            await _repository.DeleteUserVehicle(userId, plateNumber);
            return Ok();
        }
        
        /// <summary>
        /// This method updates the provided vehicle data for the provided vehicle identifier
        /// </summary>
        /// <param name="oldPlateNumber">The current saved vehicle plate number in the API</param>
        /// <param name="vehicle">The new vehicle data to be saved in the API</param>
        [HttpPost]
        [Authorize]
        [Route ("vehicles/{oldPlateNumber}")] // POST api/user/vehicles/1111-ABC
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserVehicle(string oldPlateNumber, [FromBody]Vehicle vehicle)
        {
            _log.Info("Calling update user vehicle endpoint...");
            var clientId = this.User.Claims.First(i => i.Type == "jti").Value;
            if (string.IsNullOrEmpty(vehicle.IdClient))
            {
                vehicle.IdClient = clientId;
            } else if (vehicle.IdClient != clientId && !vehicle.IdDrivers.Contains(clientId))
            {
                vehicle.IdDrivers.Add(clientId);
            }

            await _repository.UpdateVehicle(oldPlateNumber, vehicle);
            return Ok();
        }

        /// <summary>
        /// This method gets the dates of the provided user identifier
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>A list with the user dates</returns>
        [HttpGet]
        [Authorize]
        [Route ("dates")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<Date>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Date>>> GetUserDates(string userId)
        {
            _log.Info("Calling get user dates endpoint...");

            var dates = await _repository.GetUserDates(userId);
            return Ok(dates);
        }
        
        /// <summary>
        /// This method saves the provided date data in the API
        /// </summary>
        /// <param name="date">The date information</param>
        [HttpPost]
        [Authorize]
        [Route ("dates")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> NewUserDate([FromBody]Date date)
        {
            _log.Info("Calling new user date endpoint...");

            try
            {
                await _repository.NewDate(date);
            }
            catch (ArgumentException)
            {
                _log.Error("Date update user date endpoint...");
                return BadRequest();
            }

            var reparation = new Reparation
            {
                Id = Guid.NewGuid().ToString(),
                WorkshopId = date.WorkshopId,
                PlateNumber = date.PlateNumber,
                Date = DateTime.UtcNow,
                Type = date.Reason,
                Status = date.Status,
                Details = new List<string>()
            };
            await _repository.NewReparation(reparation);
            return Ok();
        }
    }
}