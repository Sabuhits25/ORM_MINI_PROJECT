using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Configuration;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Utils;

namespace ORM_MINI_PROJECT.Contex
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.connectionString);
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
