using System.Threading;
using System.Threading.Tasks;
using Common.Contracts.Events;
using MessageBroker.Infrastructure.Interfaces;
using Newtonsoft.Json.Linq;
using VikleAPIMS.Data;

namespace VikleAPIMS.Web.Handlers
{
    /// <summary>
    /// This class contains the implementation of the registered user event for event driven architecture
    /// </summary>
    public class RegisteredUserHandler : IServiceEventHandler
    {
        private IVikleRepository _repository;

        public RegisteredUserHandler(IVikleRepository repository)
        {
            _repository = repository;
        }
        
        /// <summary>
        /// This method handles the registered user event.
        /// </summary>
        /// <param name="jObject">The JObject</param>
        /// <param name="cancellationToken">The cancellation token</param>
        public async Task Handle(JObject jObject, CancellationToken cancellationToken)
        {
            var userRegistered = jObject.ToObject<UserRegisteredEvent>();
            await _repository.NewUser(userRegistered.User, cancellationToken);
        }
    }
}