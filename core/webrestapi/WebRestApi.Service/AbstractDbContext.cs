using Microsoft.EntityFrameworkCore;
using WebRestApi.Service.Models;

namespace WebRestApi.Service
{
    public abstract class AbstractDbContext : DbContext
    {
        public AbstractDbContext(DbContextOptions<AbstractDbContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SendMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Message>()
            //    .HasOne(m => m.Receiver)
            //    .WithMany(u => u.ReceivedMessages)
            //    .HasForeignKey(m => m.ReceiverId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }
}