using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Models.Models;

namespace Date
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext()
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
        public DbSet<SaveCourseUser> SaveCourseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseUser>()
                        .HasOne(x => x.Course)
                        .WithMany(u => u.CourseUser)
                        .HasForeignKey(k => k.CourseId);

            modelBuilder.Entity<CourseUser>()
                        .HasOne(x => x.User)
                        .WithMany(c => c.UserCourse)
                        .HasForeignKey(k => k.UserId);

            // SEED
            var course = new Course()
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = "JavaScript",
                Duration = "6",
                Description = "",
                Price = 0
            };

            var AdminRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin" };
            var UserRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User" };

            modelBuilder.Entity<Course>().HasData(course);
            modelBuilder.Entity<IdentityRole>().HasData(AdminRole, UserRole);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4EJHC35;Database=Academy;Trusted_Connection=True");
        }
    }
}