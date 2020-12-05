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
        Task<Vehicle> GetVehicleByPlateNumber(string plateNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the user data for the given user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user data</returns>
        Task<User> GetUserById(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method updates the provided user data in the database.
        /// </summary>
        /// <param name="user">The user data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateUser(User user, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the user data for the given user email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user data</returns>
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the user vehicles for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user vehicles</returns>
        Task<List<Vehicle>> GetUserVehicles(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method deletes the vehicle with the provided plate number for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task DeleteUserVehicle(string userId, string plateNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method updates the provided vehicle data in the database.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="vehicle">The vehicle data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateVehicle(string plateNumber, Vehicle vehicle, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the provided user dates
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user dates</returns>
        Task<List<Date>> GetUserDates(string userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method inserts the provided date information in the databse
        /// </summary>
        /// <param name="date">The date information</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewDate(Date date, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method updates the date status of the provided vehicle in the database
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="status">The reparation status</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task UpdateDateStatus(string plateNumber, ReparationStatus status, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method gets the date data with provided plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The date data</returns>
        Task<Date> GetDateByPlateNumber(string plateNumber, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method inserts a new workshop in the database.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="name">The workshop name</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewWorkshop(string workshopId, string name, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// This method gets the workshop data for the given identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The workshop data</returns>
        Task<Workshop> GetWorkshopById(string workshopId, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method inserts a new user in the database.
        /// </summary>
        /// <param name="user">The user data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        Task NewUser(User user, CancellationToken cancellationToken = default);
    }
}