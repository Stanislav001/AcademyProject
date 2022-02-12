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
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseStudent>()
                        .HasKey(bc => new { bc.CourseId, bc.StudentId });

            modelBuilder.Entity<CourseStudent>()
                        .HasOne(bc => bc.Course)
                        .WithMany(b => b.CourseStudents)
                        .HasForeignKey(bc => bc.CourseId); 

            modelBuilder.Entity<CourseStudent>()
                        .HasOne(bc => bc.Student)
                        .WithMany(c => c.CourseStudents)
                        .HasForeignKey(bc => bc.StudentId);

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
                Price = 800

            };

            var student = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Ivan",
                SecondName = "Hristov",
                LastName = "Petrov",
                City = "Sofia",
                Email = "petrov@gmail.com",
                PhoneNumber = "302-444-1234",
                Year = 19
            };

            var AdminRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin" };
            var UserRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User" };

            modelBuilder.Entity<Course>().HasData(course);
            modelBuilder.Entity<Student>().HasData(student);
            modelBuilder.Entity<IdentityRole>().HasData(AdminRole, UserRole);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4EJHC35;Database=Academy;Trusted_Connection=True");
        }
    }
}