using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace VikleAPIMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        public VehicleController()
        {
        }
        
        [HttpGet]
        [Route ("reparations")]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetVehicleReparations(string plateNumber)
        {
            return Ok(new List<Reparation>());
        }
        
        [HttpGet]
        [Route ("reparations")]
        public async Task<ActionResult<User>> GetVehicleOwner(string plateNumber)
        {
            return Ok(new User());
        }
    }
}