using DAL.Migrations;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security;

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
        public DbSet<PermissionType> PermissionTypes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<il> ils { get; set; }
        public DbSet<ilce> ilces { get; set; }
        public DbSet<Worksite> Worksites { get; set; }
        public DbSet<WorksiteWorker> WorksiteWorkers { get; set; }
        public DbSet<WorksiteWorkerType> WorksiteWorkerTypes { get; set; }
        public DbSet<WorksiteAction> WorksiteActions { get; set; }
        public DbSet<WorksiteActionType> WorksiteActionTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("user");
                user.Property(u => u.Salary).HasDefaultValue(0);
            });
            modelBuilder.Entity<Role>(userRole =>
            {
                userRole.ToTable("role");
            });
            modelBuilder.Entity<VerifyCode>(verifyCode =>
            {
                verifyCode.ToTable("verifyCode");
            });
            modelBuilder.Entity<PermissionType>(permissionType =>
            {
                permissionType.ToTable("permissionType");
            });
            modelBuilder.Entity<Permission>(permission =>
            {
                permission.ToTable("permission");
            });
            modelBuilder.Entity<il>(il =>
            {
                il.ToTable("il");
                il.Property(e => e.Id)
            .ValueGeneratedNever(); // This will disable auto-increment for the Id field

            });
            modelBuilder.Entity<ilce>(ilce =>
            {
                ilce.ToTable("ilce");
            });
            modelBuilder.Entity<Worksite>(worksites =>
            {
                worksites.ToTable("Worksites");
            });
            modelBuilder.Entity<WorksiteWorker>(worksiteWorker =>
            {
                worksiteWorker.ToTable("WorksiteWorker");
            });
            modelBuilder.Entity<WorksiteWorkerType>(worksiteWorkerType =>
            {
                worksiteWorkerType.ToTable("WorksiteWorkerType");
            });
            modelBuilder.Entity<WorksiteAction>(worksiteAction =>
            {
                worksiteAction.ToTable("WorksiteAction");
            });
            modelBuilder.Entity<WorksiteActionType>(worksiteActionType =>
            {
                worksiteActionType.ToTable("WorksiteActionType");
                worksiteActionType.Property(e => e.Id)
            .ValueGeneratedNever(); // This will disable auto-increment for the Id field
            });
        }
    }
}
