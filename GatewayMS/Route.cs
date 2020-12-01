namespace GatewayMS
{
    /// <summary>
    /// This class contains the implementation of the Route data model
    /// </summary>
    public class Route
    {
        /// <summary>
        /// Gets or sets the endpoint
        /// </summary>
        public string Endpoint { get; set; }
        
        /// <summary>
        /// Gets or sets the destination object
        /// </summary>
        public Destination Destination { get; set; }
    }
}