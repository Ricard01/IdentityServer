using Identity.Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVE.Identity.Infrastructure.Persistence.Configuration
{
    public class ApplicationPermissionConfiguration : IEntityTypeConfiguration<ApplicationPermission>
    {
        public void Configure(EntityTypeBuilder<ApplicationPermission> builder)
        {
            builder.Property(ap => ap.Name).HasMaxLength(200).IsRequired();

            builder.Property( ap => ap.Module).HasMaxLength(200).IsRequired();            

            builder.Property( ap => ap.ClaimType).HasMaxLength(250).IsRequired();

            builder.Property( ap => ap.ClaimValue).HasMaxLength(250).IsRequired();

        }
    }

}
