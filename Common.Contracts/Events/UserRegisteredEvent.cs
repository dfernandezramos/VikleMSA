using MessageBroker.Contracts;
using MessageBroker.Contracts.Events;

namespace Common.Contracts.Events
{
    /// <summary>
    /// This class contains the implementation of the user registered event used for the event driven architecture
    /// </summary>
    [Event("UserRegistered")]
    public class UserRegisteredEvent : IEvent
    {
        /// <summary>
        /// Gets or sets the user data model
        /// </summary>
        public User User { get; set; }
    }
}