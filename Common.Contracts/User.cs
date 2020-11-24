namespace Common.Contracts
{
    /// <summary>
    /// This class contains the definition of the User data model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the identifier of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the surname of the user.
        /// </summary>
        public string Surname { get; set; }
        
        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Gets or sets a boolean indicating whether this user is worker or not.
        /// </summary>
        public bool IsWorker { get; set; }
        
        /// <summary>
        /// Gets or sets the id of the workshop the user with worker condition works in.
        /// </summary>
        public string IdWorkshop { get; set; }
    }
}