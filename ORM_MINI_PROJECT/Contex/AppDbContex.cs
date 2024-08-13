using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Contex
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get;  set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment>Payments { get; set; }
        public DbSet<User> Users { get;  set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
