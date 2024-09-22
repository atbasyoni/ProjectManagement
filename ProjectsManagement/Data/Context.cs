using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Configurations;
using ProjectsManagement.Models;

namespace ProjectsManagement.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PictureConfiguration());
            modelBuilder.ApplyConfiguration(new BlockedUsersConfiguration());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<PasswordChangeRequest> PasswordChanges { get; set; }
    }
}
