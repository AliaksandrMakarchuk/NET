using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace WebRestApi.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            .UseKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Loopback, 3010);
                })
                .UseStartup<Startup>();
        }
    }

}
