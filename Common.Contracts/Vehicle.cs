using System;
using System.Collections.Generic;

namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the vehicle data model.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the plate number of the vehicle.
        /// </summary>
        public string PlateNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the model of the vehicle.
        /// </summary>
        public string Model { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        public VehicleType VehicleType { get; set; }
        
        /// <summary>
        /// Gets or sets the year of the vehicle.
        /// </summary>
        public int Year { get; set; } = DateTime.Today.Year;
        
        /// <summary>
        /// Gets or sets the last TBDS date of the vehicle.
        /// </summary>
        public DateTime LastTBDS { get; set; }
        
        /// <summary>
        /// Gets or sets the last ITV date of the vehicle.
        /// </summary>
        public DateTime LastITV { get; set; }
        
        /// <summary>
        /// Gets or sets the identifier of the vehicle owner.
        /// </summary>
        public string IdClient { get; set; }
        
        /// <summary>
        /// Gets or sets the identifiers of the vehicle additional drivers.
        /// </summary>
        public List<string> IdDrivers { get; set; }

        /// <summary>
        /// Gets a value indicating if this model has all required fields properly filled
        /// </summary>
        public bool IsComplete => !string.IsNullOrEmpty(Model) &&
                                  !string.IsNullOrEmpty(PlateNumber);
    }
}