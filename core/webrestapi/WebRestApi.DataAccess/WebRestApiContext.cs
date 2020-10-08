using System.IO;
using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;

namespace WebRestApi.DataAccess
{
    public class WebRestApiContext : AbstractDbContext
    {
        public WebRestApiContext(DbContextOptions<AbstractDbContext> options)
        : base(options) { 
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "messaging.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            // optionsBuilder.UseSqlite($"Data Source={System.AppDomain.CurrentDomain.BaseDirectory}/messaging.db");
        }
    }
}
