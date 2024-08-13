using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Configuration
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(a => a.FullName).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Email).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Password).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Address).IsRequired(true).HasMaxLength(100);
            builder.Property(a => a.Username).IsRequired(true).HasMaxLength(100);
           



        }
    }
}
