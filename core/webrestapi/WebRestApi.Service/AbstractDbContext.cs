using Microsoft.EntityFrameworkCore;
using WebRestApi.Service.Models;

namespace WebRestApi.Service
{
    public abstract class AbstractDbContext : DbContext
    {
        public AbstractDbContext(DbContextOptions<AbstractDbContext> options) : base(options) { }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Message>()
            //     .HasOne(m => m.Sender)
            //     .WithMany(u => u.SendMessages)
            //     .HasForeignKey(m => m.SenderId)
            //     .OnDelete(DeleteBehavior.SetNull);

            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

            base.OnModelCreating(modelBuilder);
        }
    }
}