using System.Threading.Tasks;
using Common.Domain;
using Microsoft.Extensions.Configuration;
using VikleAPIMS.Data;

namespace VikleAPIMS.Web
{
    /// <summary>
    /// This class contains the database populator for the vikle API microservice
    /// </summary>
    public class DatabasePopulator
    {
        private readonly IVikleRepository _repository;
        private readonly IConfiguration _configuration;
        private ILog _log;

        public DatabasePopulator(IVikleRepository repository, ILog log)
        {
            _repository = repository;
            _log = log;
        }

        /// <summary>
        /// This method populates the database.
        /// </summary>
        public async Task Seed()
        {
            _log.Info("Populating Login database...");
            
            _log.Info("Login database population success");
        }
    }
}