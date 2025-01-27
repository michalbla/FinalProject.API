using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TNAI_FinalProject.Model.Configurations;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Model
{
    public class AppDbContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RoleUser> Roles { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }

        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleUserConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
