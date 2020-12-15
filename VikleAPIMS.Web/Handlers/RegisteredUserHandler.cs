// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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