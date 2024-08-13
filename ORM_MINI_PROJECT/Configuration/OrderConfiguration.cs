using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(a => a.UserId).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.OrderDate).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.TotalAmount).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Status).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.OrderDetails).IsRequired(true).HasMaxLength(100);

            builder.HasCheckConstraint("CK_ToalAmount", "TotalAmount>= 0");

        }
    }
}
