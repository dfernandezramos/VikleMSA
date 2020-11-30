using MessageBroker.Contracts;
using MessageBroker.Contracts.Events;

namespace Common.Contracts.Events
{
    [Event("UserRegistered")]
    public class UserRegisteredEvent : IEvent
    {
        public User User { get; set; }
    }
}