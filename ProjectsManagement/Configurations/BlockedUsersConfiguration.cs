using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectsManagement.Models;
using System.Reflection.Emit;

namespace ProjectsManagement.Configurations
{
    public class BlockedUsersConfiguration : IEntityTypeConfiguration<BlockedUser>
    {
        public void Configure(EntityTypeBuilder<BlockedUser> builder)
        {
            builder.HasKey(b => b.ID);  

            builder.HasOne(b => b.Blocker)
                   .WithMany(u => u.BlockedUsers) 
                   .HasForeignKey(b => b.BlockerID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder .HasOne(b => b.Blocked)
                    .WithMany(u => u.BlockedByUsers)  
                    .HasForeignKey(b => b.BlockedID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
