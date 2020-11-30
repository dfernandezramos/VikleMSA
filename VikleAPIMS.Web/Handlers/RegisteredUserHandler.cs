using System.Threading;
using System.Threading.Tasks;
using Common.Contracts.Events;
using MessageBroker.Infrastructure.Interfaces;
using Newtonsoft.Json.Linq;
using VikleAPIMS.Data;

namespace VikleAPIMS.Web.Handlers
{
    public class RegisteredUserHandler : IServiceEventHandler
    {
        private IVikleRepository _repository;

        public RegisteredUserHandler(IVikleRepository repository)
        {
            _repository = repository;
        }
        
        public async Task Handle(JObject jObject, CancellationToken cancellationToken)
        {
            var userRegistered = jObject.ToObject<UserRegisteredEvent>();
            await _repository.NewUser(userRegistered.User, cancellationToken);
        }
    }
}