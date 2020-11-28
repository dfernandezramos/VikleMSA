using System.Threading.Tasks;
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

        public DatabasePopulator(IVikleRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This method populates the database.
        /// </summary>
        public async Task Seed()
        {
            
        }
    }
}