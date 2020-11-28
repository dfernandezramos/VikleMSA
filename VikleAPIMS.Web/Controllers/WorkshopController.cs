using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VikleAPIMS.Web.Controllers
{
    /// <summary>
    /// This class contains the controller with the workshop endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WorkshopController : ControllerBase
    {
        public WorkshopController()
        {
        }
        
        /// <summary>
        /// This method gets the workshop current reparations for the given workshop identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <returns>The workshop current reparations</returns>
        [HttpGet]
        [Route ("reparations")]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetWorkshopReparations(string workshopId)
        {
            return Ok(new List<Reparation>());
        }
        
        /// <summary>
        /// This method posts the provided reparation information for the workshop reparation to the API.
        /// </summary>
        /// <param name="reparationId">The reparation identifier</param>
        /// <param name="reparationDate">The reparation date</param>
        /// <param name="plateNumber">The plate number</param>
        /// <param name="status">The reparation status</param>
        /// <param name="reparationType">The reparation type</param>
        [HttpPost]
        [Route ("reparations")]
        public async Task<IActionResult> UpdateWorkshopReparation(string reparationId, DateTime reparationDate, string plateNumber, ReparationStatus status, ReparationType reparationType)
        {
            return Ok();
        }
    }
}