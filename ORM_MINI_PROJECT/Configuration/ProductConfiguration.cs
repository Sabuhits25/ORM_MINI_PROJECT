using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.Id).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Price).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Stock).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.CreatedDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(a => a.UpdatedDate).HasDefaultValue(DateTime.UtcNow);


            builder.HasCheckConstraint("CK_Price", "Price > 0");
            builder.HasCheckConstraint("CK_StockCount", "Stock >= 0");

        }
    }
}
