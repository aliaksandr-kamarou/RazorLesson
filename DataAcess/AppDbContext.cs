using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RazorLesson.Models;

namespace RazorLesson.DataAcess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Role>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<UserRoles>()
                .HasKey(c => new {c.UserId, c.RoleId});

            modelBuilder.Entity<UserRoles>()
                .HasOne(c => c.User)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<UserRoles>()
                .HasOne(c => c.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<Role>()
                .HasData(new List<Role>()
                {
                    new Role { Id = 1, Name = "Admin" },
                    new Role { Id = 2, Name = "Manager" }
                });

            modelBuilder.Entity<UserRoles>()
                .HasData(new List<UserRoles>()
                {
                    new UserRoles { RoleId = 1, UserId = 1 },
                    new UserRoles { RoleId = 2, UserId = 2 }
                });

            modelBuilder.Entity<User>()
                .HasData(new List<User>()
                {
                    new User { Id = 1, Email = "admin@email.com", Name = "Admin", Password = "admin" },
                    new User { Id = 2, Email = "manager@email.com", Name = "Manager", Password = "manager" }
                });
        }
    }
}
