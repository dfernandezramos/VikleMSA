// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
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
    /// This class contains the controller with the workshop endpoints.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = UserRole.Worker)]
    public class WorkshopController : ControllerBase
    {
        private readonly ILog _log;
        private readonly IVikleRepository _repository;
        
        public WorkshopController(ILog log, IVikleRepository repository)
        {
            _log = log;
            _repository = repository;
        }
        
        /// <summary>
        /// This method gets the workshop current reparations for the given workshop identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <returns>The workshop current reparations</returns>
        [HttpGet]
        [Route ("reparations")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(List<Reparation>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Reparation>>> GetWorkshopReparations(string workshopId)
        {
            _log.Info("Calling get workshop reparations endpoint");
            var reparations = await _repository.GetReparationsByWorkshopId(workshopId);
            
            return Ok(reparations);
        }
        
        /// <summary>
        /// This method posts the provided reparation information for the workshop reparation to the API.
        /// </summary>
        /// <param name="reparation">The reparation data</param>
        [HttpPost]
        [Route ("reparations")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWorkshopReparation([FromBody]Reparation reparation)
        {
            _log.Info("Calling update workshop reparation endpoint");
            
            if (reparation.Id == default)
            {
                reparation.Id = Guid.NewGuid().ToString();
                await _repository.NewReparation(reparation);
            }
            else
            {
                try
                {
                    await _repository.UpdateWorkshopReparation(reparation);
                    await _repository.UpdateDateStatus(reparation.PlateNumber, reparation.Status);
                }
                catch (ArgumentException e)
                {
                    _log.Error("Reparation with the provided id does not exists");
                    return Forbid();
                }
            }
            
            return Ok();
        }
    }
}