using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VikleAPIMS.Web
{
    /// <summary>
    /// This class contains the main program initialization.
    /// </summary>
    class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}