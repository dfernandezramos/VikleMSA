// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
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