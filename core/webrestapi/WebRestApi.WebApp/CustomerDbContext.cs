using Microsoft.EntityFrameworkCore;
using WebRestApi.WebApp.Models;

namespace WebRestApi.WebApp
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options)
        : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}