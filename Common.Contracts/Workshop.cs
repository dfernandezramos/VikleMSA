// Copyright (C)  2020  David Fernández Ramos.
// Permission is granted to copy, distribute and/or modify this document
// under the terms of the GNU Free Documentation License, Version 1.3
// or any later version published by the Free Software Foundation;
// with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.
// A copy of the license is included in the section entitled "GNU
// Free Documentation License".
namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the workshop data model
    /// </summary>
    public class Workshop
    {
        /// <summary>
        /// Gets or sets the workshop identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the workshop name
        /// </summary>
        public string Name { get; set; }
    }
}