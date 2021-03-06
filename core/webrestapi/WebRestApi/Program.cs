﻿using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
            .UseKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Loopback, 3000);
            })
            .UseStartup<Startup>();
    }
}