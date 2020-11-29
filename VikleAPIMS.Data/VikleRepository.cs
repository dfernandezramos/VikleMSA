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
    }
}