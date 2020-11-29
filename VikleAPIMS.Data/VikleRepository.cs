using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Infrastructure.MongoDB;
using MongoDB.Driver;

namespace VikleAPIMS.Data
{
    /// <summary>
    /// This class contains the implementation of the Vikle database repository.
    /// </summary>
    public class VikleRepository : BaseRepository, IVikleRepository
    {
        IMongoCollection<Reparation> Reparations => MongoDatabase.GetCollection<Reparation>("Reparations");
        IMongoCollection<Vehicle> Vehicles => MongoDatabase.GetCollection<Vehicle>("Vehicles");
        IMongoCollection<User> Users => MongoDatabase.GetCollection<User>("Users");

        protected VikleRepository(MongoDbSettings settings) : base(settings)
        {
        }
        
        /// <summary>
        /// This method updates the provided workshop reparation information in the database.
        /// </summary>
        /// <param name="reparation">The reparation data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task UpdateWorkshopReparation(Reparation reparation, CancellationToken cancellationToken = default)
        {
            var result = await GetReparationById(reparation.Id, cancellationToken);
            
            if (result == default(Reparation))
            {
                throw new ArgumentException("No reparation with the provided id was found");
            }
            
            await Reparations.ReplaceOneAsync(c => c.Id == reparation.Id, reparation, new ReplaceOptions { IsUpsert = false }, cancellationToken);
        }
        
        /// <summary>
        /// This method saves in the database the provided reparation data.
        /// </summary>
        /// <param name="reparation">The reparation data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task NewReparation(Reparation reparation, CancellationToken cancellationToken = default)
        {
            await Reparations.InsertOneAsync(reparation, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        /// <summary>
        /// This method gets the reparation data for the given identifier.
        /// </summary>
        /// <param name="reparationId">The reparation identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The reparation data</returns>
        public async Task<Reparation> GetReparationById(string reparationId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Reparations.Find(c => c.Id == reparationId, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }
        
        /// <summary>
        /// This method gets the reparation data for the given workshop identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The reparation data</returns>
        public async Task<List<Reparation>> GetReparationsByWorkshopId(string workshopId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(await Reparations.Find(c => c.WorkshopId == workshopId, new FindOptions { AllowPartialResults = false }).ToListAsync(cancellationToken));
        }

        /// <summary>
        /// This method gets the current reparation for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle current reparation</returns>
        public async Task<Reparation> GetVehicleCurrentReparation(string plateNumber,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Reparations.Find(c => c.PlateNumber == plateNumber &&
                c.Status != ReparationStatus.Repaired || c.Status != ReparationStatus.Rejected,
                new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }

        /// <summary>
        /// This method gets the list of reparations for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle reparations</returns>
        public async Task<List<Reparation>> GetVehicleReparations(string plateNumber,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(await Reparations.Find(c => c.PlateNumber == plateNumber, new FindOptions { AllowPartialResults = false }).ToListAsync(cancellationToken));
        }

        /// <summary>
        /// This method gets the owner for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle owner</returns>
        public async Task<User> GetVehicleOwner(string plateNumber, CancellationToken cancellationToken = default)
        {
            var vehicle = await GetVehicleById(plateNumber, cancellationToken);
            return await GetUserById(vehicle.IdClient, cancellationToken);
        }
        
        /// <summary>
        /// This method gets the vehicle data for the given vehicle plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The vehicle data</returns>
        public async Task<Vehicle> GetVehicleById(string plateNumber, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Vehicles.Find(c => c.PlateNumber == plateNumber, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }
        
        /// <summary>
        /// This method gets the user data for the given user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user data</returns>
        public async Task<User> GetUserById(string userId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Users.Find(c => c.Id == userId, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken)); 
        }
    }
}