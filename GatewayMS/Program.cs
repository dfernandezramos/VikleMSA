using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace GatewayMS
{
    /// <summary>
    /// This class contains the main program class of the gateway service
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}