using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<VerifyCode> VerifyCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(userRole =>
            {
                userRole.ToTable("user");
            });
            modelBuilder.Entity<Role>(userRole =>
            {
                userRole.ToTable("role");
            });
            modelBuilder.Entity<VerifyCode>(verifyCode =>
            {
                verifyCode.ToTable("verifyCode");
            });
        }
    }
}
