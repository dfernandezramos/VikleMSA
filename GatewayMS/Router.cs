using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GatewayMS
{
    /// <summary>
    /// This class contains the implementation of the router in order to route the incoming request to the
    /// assigned destination.
    /// </summary>
    public class Router
    {
        /// <summary>
        /// Gets or sets the list of routes
        /// </summary>
        public List<Route> Routes { get; set; }

        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
        }

        /// <summary>
        /// This method routes the provided request to its assigned destination
        /// </summary>
        /// <param name="request">The incoming request</param>
        /// <returns>The request http response message</returns>
        public async Task<HttpResponseMessage> RouteRequest(HttpRequest request)
        {
            string path = request.Path.ToString();
            string basePath = '/' + path.Split('/')[1];

            Destination destination;
            try
            {
                destination = Routes.First(r => r.Endpoint.Equals(basePath)).Destination;
            }
            catch
            {
                return ConstructErrorMessage("The path could not be found.");
            }

            return await destination.SendRequest(request);
        }

        private HttpResponseMessage ConstructErrorMessage(string error)
        {
            HttpResponseMessage errorMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(error)
            };
            return errorMessage;
        }
    }
}