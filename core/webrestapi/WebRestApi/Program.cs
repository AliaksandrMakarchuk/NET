using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using WebRestApi.Logger;
using System.Net;

#pragma warning disable 1591
namespace WebRestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((context, builder) =>
            {
                builder.AddFile();
            })
            .UseKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Loopback, 3000);
            })
            .UseStartup<Startup>();
    }
}