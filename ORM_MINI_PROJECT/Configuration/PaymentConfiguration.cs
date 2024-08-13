using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(a => a.OrderId).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Amount).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.PaymentDate).IsRequired(true).HasMaxLength(100);

            builder.HasCheckConstraint("CK_Amount", "Amount >= 0");
        }
    }
}
