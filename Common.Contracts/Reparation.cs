// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
using System;
using System.Collections.Generic;

namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the reparation data model.
    /// </summary>
    public class Reparation
    {
        /// <summary>
        /// Gets or sets the identifier of the reparation.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Gets or sets the identifier of the workshop.
        /// </summary>
        public string WorkshopId { get; set; }
        
        /// <summary>
        /// Gets or sets the plate number of the repaired vehicle.
        /// </summary>
        public string PlateNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the date of the reparation.
        /// </summary>
        public long Date { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the reparation.
        /// </summary>
        public ReparationType Type { get; set; }
        
        /// <summary>
        /// Gets or sets the status of the reparation.
        /// </summary>
        public ReparationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the oil filter was changed in this reparation or not.
        /// </summary>
        public bool OilFilter { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the air filter was changed in this reparation or not.
        /// </summary>
        public bool AirFilter { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the gas filter was changed in this reparation or not.
        /// </summary>
        public bool GasFilter { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the liquids were filled in this reparation or not.
        /// </summary>
        public bool Liquids { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the TBDS was performed in this reparation or not.
        /// </summary>
        public bool TBDS { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating if the ITV was performed in this reparation or not.
        /// </summary>
        public bool ITV { get; set; }
        
        /// <summary>
        /// Gets or sets the extra details of the reparation.
        /// </summary>
        public List<string> Details { get; set; }
    }
}