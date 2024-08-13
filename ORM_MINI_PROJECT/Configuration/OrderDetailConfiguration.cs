using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(a => a.OrderId).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.ProductId).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Quantity).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.PricePerItem).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.UnitPrice).IsRequired(true).HasMaxLength(100);

            builder.HasCheckConstraint("CK_Quantity", "Quantity >= 0");
            builder.HasCheckConstraint("PricePerItem", "PricePerItem >= 0");

        }
    }
}
