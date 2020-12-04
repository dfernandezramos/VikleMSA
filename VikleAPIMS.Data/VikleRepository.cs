using System;
using System.Collections.Generic;
using System.Linq;
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
        IMongoCollection<Date> Dates => MongoDatabase.GetCollection<Date>("Dates");
        IMongoCollection<Workshop> Workshops => MongoDatabase.GetCollection<Workshop>("Workshops");

        public VikleRepository(MongoDbSettings settings) : base(settings)
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
        
        /// <summary>
        /// This method updates the provided user data in the database.
        /// </summary>
        /// <param name="user">The user data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task UpdateUser(User user, CancellationToken cancellationToken = default)
        {
            var result = await GetUserById(user.Id, cancellationToken);
            
            if (result == null)
            {
                throw new ArgumentException("No user with the provided id was found");
            }
            
            await Users.ReplaceOneAsync(c => c.Id == user.Id, user, new ReplaceOptions { IsUpsert = false }, cancellationToken);
        }
        
        /// <summary>
        /// This method inserts a new user in the database.
        /// </summary>
        /// <param name="user">The user data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task NewUser(User user, CancellationToken cancellationToken = default)
        {
            await Users.InsertOneAsync(user, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        /// <summary>
        /// This method gets the user data for the given user email.
        /// </summary>
        /// <param name="email">The user email</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user data</returns>
        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Users.Find(c => c.Email == email, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken)); 
        }

        /// <summary>
        /// This method gets the user vehicles for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user vehicles</returns>
        public async Task<List<Vehicle>> GetUserVehicles(string userId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(await Vehicles.Find(c => c.IdClient == userId || c.IdDrivers.Contains(userId), new FindOptions { AllowPartialResults = false }).ToListAsync(cancellationToken));
        }
        
        /// <summary>
        /// This method deletes the vehicle with the provided plate number for the provided user identifier.
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="plateNumber">The plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task DeleteUserVehicle(string userId, string plateNumber, CancellationToken cancellationToken = default)
        {
            var vehicle = await GetVehicleById(plateNumber, cancellationToken);

            if (vehicle.IdDrivers != null && vehicle.IdDrivers.Contains(userId))
            {
                vehicle.IdDrivers.Remove(userId);
                await UpdateVehicle(vehicle.PlateNumber, vehicle, cancellationToken);
            }
            else
            {
                if (vehicle.IdDrivers != null && vehicle.IdDrivers.Any())
                {
                    var id = vehicle.IdDrivers.First();
                    vehicle.IdClient = id;
                    vehicle.IdDrivers.Remove(userId);
                    await UpdateVehicle(vehicle.PlateNumber, vehicle, cancellationToken);
                }
                else
                {
                    await Vehicles.DeleteOneAsync(c => c.PlateNumber == plateNumber, cancellationToken);
                }
            }
        }
        
        /// <summary>
        /// This method updates the provided vehicle data in the database.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="vehicle">The vehicle data</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task UpdateVehicle(string plateNumber, Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            var result = await GetVehicleById(plateNumber, cancellationToken);
            
            if (result == null)
            {
                await Vehicles.InsertOneAsync(vehicle, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
            }
            else
            {
                await Vehicles.ReplaceOneAsync(c => c.PlateNumber == vehicle.PlateNumber, vehicle, new ReplaceOptions { IsUpsert = false }, cancellationToken);
            }
        }
        
        /// <summary>
        /// This method gets the provided user dates
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The user dates</returns>
        public async Task<List<Date>> GetUserDates(string userId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(await Dates.Find(c => c.IdClient == userId, new FindOptions { AllowPartialResults = false }).ToListAsync(cancellationToken));
        }

        /// <summary>
        /// This method inserts the provided date information in the database
        /// </summary>
        /// <param name="date">The date information</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task NewDate(Date date, CancellationToken cancellationToken = default)
        {
            var result = await GetDateById(date.PlateNumber, cancellationToken);
            
            if (result != null)
            {
                throw new ArgumentException("The provided vehicle already has a date");
            }
            
            await Dates.InsertOneAsync(date, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }
        
        /// <summary>
        /// This method updates the date status of the provided vehicle in the database
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="status">The reparation status</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task UpdateDateStatus(string plateNumber, ReparationStatus status, CancellationToken cancellationToken = default)
        {
            var result = await GetDateById(plateNumber, cancellationToken);
            
            if (result == null)
            {
                throw new ArgumentException("The provided vehicle has not a date");
            }

            result.Status = status;
            await Dates.ReplaceOneAsync(c => c.Id == result.Id, result, new ReplaceOptions { IsUpsert = false }, cancellationToken);
        }
        
        /// <summary>
        /// This method gets the date data with provided plate number.
        /// </summary>
        /// <param name="plateNumber">The vehicle plate number</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The date data</returns>
        public async Task<Date> GetDateById(string plateNumber, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Dates.Find(c => c.PlateNumber == plateNumber, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }

        /// <summary>
        /// This method inserts a new workshop in the database.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="name">The workshop name</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task NewWorkshop(string workshopId, string name, CancellationToken cancellationToken = default)
        {
            var workshop = new Workshop
            {
                Id = workshopId,
                Name = name
            };
            await Workshops.InsertOneAsync(workshop, new InsertOneOptions { BypassDocumentValidation = false }, cancellationToken);
        }

        /// <summary>
        /// This method gets the workshop data for the given identifier.
        /// </summary>
        /// <param name="workshopId">The workshop identifier</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The workshop data</returns>
        public async Task<Workshop> GetWorkshopById(string workshopId, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(Workshops.Find(c => c.Id == workshopId, new FindOptions { AllowPartialResults = false }).FirstOrDefault(cancellationToken));
        }
    }
}