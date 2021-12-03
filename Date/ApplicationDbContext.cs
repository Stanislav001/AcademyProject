using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;

namespace Date
{
    public class ApplicationDbContext :  IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var AdminRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin" };
            var UserRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User" };

            modelBuilder.Entity<IdentityRole>().HasData(AdminRole, UserRole);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4EJHC35;Database=Academy;Trusted_Connection=True");
        }
    }
}