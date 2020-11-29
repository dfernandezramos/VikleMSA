using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;

namespace VikleAPIMS.Data
{
    /// <summary>
    /// This interface contains the definition of the Vikle database repository.
    /// </summary>
    public interface IVikleRepository
    {
        /// <summary>
        /// This method updates the provided workshop reparation information in the database.
        /// </summary>
        /// <param name="reparation">The reparation data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateWorkshopReparation(Reparation reparation, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method saves in the database the provided reparation data.
        /// </summary>
        /// <param name="reparation">The reparation data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewReparation(Reparation reparation, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the reparation data for the given identifier.
        /// </summary>
        /// <param name="reparationId">The reparation identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The reparation data</returns>
        Task<Reparation> GetReparationById(string reparationId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the reparations data for the given workshop identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The reparations data</returns>
        Task<List<Reparation>> GetReparationsByWorkshopId(string workshopId, CancellationToken cancellationToken = default);
    }
}