using System.Collections.Generic;
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
    /// This class contains the controller for the vehicle endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly ILog _log;
        private readonly IVikleRepository _repository;
        
        public VehicleController(ILog log, IVikleRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        
        /// <summary>
        /// This method gets the reparations of the provided vehicle identifier.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number.</param>
        /// <returns>A list with the vehicle reparations</returns>
        [HttpGet]
        [Route ("reparations")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Reparation>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetVehicleReparations(string plateNumber)
        {
            _log.Info("Calling get vehicle reparations endpoint...");

            var reparations = await _repository.GetVehicleReparations(plateNumber);
            return Ok(reparations);
        }
        
        /// <summary>
        /// This method gets the owner of the provided vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number.</param>
        /// <returns>The vehicle's owner</returns>
        [HttpGet]
        [Route ("owner")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Reparation>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetVehicleOwner(string plateNumber)
        {
            _log.Info("Calling get vehicle owner endpoint...");

            var owner = await _repository.GetVehicleOwner(plateNumber);
            return Ok(owner);
        }
        
        /// <summary>
        /// This method gets the current reparation for the provided vehicle identifier
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <returns>The current reparation of the vehicle</returns>
        [HttpGet]
        [Route ("reparations/current")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Reparation>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Reparation>> GetVehicleCurrentReparation(string plateNumber)
        {
            _log.Info("Calling get vehicle current reparation endpoint...");
            
            var reparation = await _repository.GetVehicleCurrentReparation(plateNumber);
            return Ok(reparation);
        }
    }
}