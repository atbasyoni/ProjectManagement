using Microsoft.EntityFrameworkCore;
using ProjectsManagement.Models;

namespace ProjectsManagement.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> option) : base(option)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<PasswordChangeRequest> PasswordChanges { get; set; }
    }
}
