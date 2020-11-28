using Common.Infrastructure.MongoDB;

namespace VikleAPIMS.Data
{
    /// <summary>
    /// This class contains the implementation of the Vikle database repository.
    /// </summary>
    public class VikleRepository : BaseRepository, IVikleRepository
    {
        protected VikleRepository(MongoDbSettings settings) : base(settings)
        {
        }
    }
}