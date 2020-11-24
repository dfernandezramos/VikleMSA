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
        /// Gets or sets the reparation datetime of the date.
        /// </summary>
        public DateTime ReparationDate { get; set; }
        
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