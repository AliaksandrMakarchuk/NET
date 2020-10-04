using Microsoft.EntityFrameworkCore;
using WebRestApi.Service;

namespace WebRestApi.DataAccess
{
    public class WebRestApiContext : AbstractDbContext
    {
        public WebRestApiContext(DbContextOptions<AbstractDbContext> options)
        : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=messaging.db");
        }
    }
}
