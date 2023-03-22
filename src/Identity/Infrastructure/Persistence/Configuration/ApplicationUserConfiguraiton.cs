using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVE.Identity.Infrastructure.Persistence.Configuration
{
    public class ApplicationUserConfiguraiton : IEntityTypeConfiguration<ApplicationUser>
    {


        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.Nombre).IsRequired();

            //builder.Property(u => u.ProfilePictureUrl)
            //    .IsRequired()
            //    .HasDefaultValue("../../../assets/images/avatars/user.png");

        }

    }   
}
