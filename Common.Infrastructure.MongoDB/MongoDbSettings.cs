namespace Common.Infrastructure.MongoDB
{
    /// <summary>
    /// This class implements the Mongo database settings data model.
    /// </summary>
    public class MongoDbSettings
    {
        /// <summary>
        /// Gets or sets the server connection string.
        /// </summary>
        public string ServerConnection { get; set; }
        
        /// <summary>
        /// Gets or sets the database name.
        /// </summary>
        public string Database { get; set; }
    }
}