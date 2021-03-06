﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;

namespace WebRestApi.DataAccess
{
    public class WebRestApiContext : AbstractDbContext
    {
        public WebRestApiContext(DbContextOptions<AbstractDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "messaging.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}