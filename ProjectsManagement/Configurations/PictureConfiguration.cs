using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Models;

namespace ProjectsManagement.Configurations
{
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(p => p.ID);

            builder.HasOne(p => p.User)
                .WithOne(u => u.Picture)
                .HasForeignKey<User>(u => u.PictureID);
        }
    }
}
