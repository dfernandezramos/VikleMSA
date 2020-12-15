// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;

namespace Common.Contracts
{
    /// <summary>
    /// This class contains the implementation of the workshop date data model.
    /// </summary>
    public class Date
    {
        /// <summary>
        /// Gets or sets the identifier of the date.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the workshop identifier of the date.
        /// </summary>
        public string WorkshopId { get; set; }
        
        /// <summary>
        /// Gets or sets the reparation datetime of the date.
        /// </summary>
        public long ReparationDate { get; set; }
        
        /// <summary>
        /// Gets or sets the reason of the date.
        /// </summary>
        public ReparationType Reason { get; set; }
        
        /// <summary>
        /// Gets or sets the plate number of the car of the date.
        /// </summary>
        public string PlateNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the client identifier who requested the date.
        /// </summary>
        public string IdClient { get; set; }
        
        /// <summary>
        /// Gets or sets the reparation status related to this date.
        /// </summary>
        public ReparationStatus Status { get; set; }
    }
}