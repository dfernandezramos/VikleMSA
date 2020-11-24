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
    public class WorkshopController : ControllerBase
    {
        public WorkshopController()
        {
        }
        
        [HttpGet]
        [Route ("reparations")]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetWorkshopReparations(string workshopId)
        {
            return Ok(new List<Reparation>());
        }
        
        [HttpPost]
        [Route ("reparations")]
        public async Task<IActionResult> UpdateWorkshopReparation(string reparationDate, string plateNumber, string status, string reparationType)
        {
            return Ok();
        }
    }
}