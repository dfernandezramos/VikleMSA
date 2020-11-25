using Common.Infrastructure.MongoDB;

namespace VikleAPIMS.Data
{
    public class VikleRepository : BaseRepository, IVikleRepository
    {
        protected VikleRepository(MongoDbSettings settings) : base(settings)
        {
        }
    }
}