using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VikleAPIMS.Web.Controllers
{
    /// <summary>
    /// This class contains the controller for the vehicle endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        public VehicleController()
        {
        }
        
        /// <summary>
        /// This method gets the reparations of the provided vehicle identifier.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number.</param>
        /// <returns>A list with the vehicle reparations</returns>
        [HttpGet]
        [Route ("reparations")]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetVehicleReparations(string plateNumber)
        {
            return Ok(new List<Reparation>());
        }
        
        /// <summary>
        /// This method gets the owner of the provided vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number.</param>
        /// <returns>The vehicle's owner</returns>
        [HttpGet]
        [Route ("owner")]
        public async Task<ActionResult<User>> GetVehicleOwner(string plateNumber)
        {
            return Ok(new User());
        }
        
        /// <summary>
        /// This method gets the current reparation for the provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns>The current reparation of the vehicle</returns>
        [HttpGet]
        [Route ("reparations/current")]
        public async Task<ActionResult<Reparation>> GetVehicleCurrentReparation(string plateNumber)
        {
            return Ok(new Reparation());
        }
    }
}