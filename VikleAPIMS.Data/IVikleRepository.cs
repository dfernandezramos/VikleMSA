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

        /// <summary>
        /// This method gets the current reparation for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle current reparation</returns>
        Task<Reparation> GetVehicleCurrentReparation(string plateNumber, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// This method gets the list of reparations for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle reparations</returns>
        Task<List<Reparation>> GetVehicleReparations(string plateNumber, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// This method gets the owner for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle owner</returns>
        Task<User> GetVehicleOwner(string plateNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the vehicle data for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle data</returns>
        Task<Vehicle> GetVehicleById(string plateNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the user data for the given user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user data</returns>
        Task<User> GetUserById(string userId, CancellationToken cancellationToken = default);
    }
}