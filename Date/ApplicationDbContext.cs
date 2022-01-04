using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

using Models.Models;

namespace Date
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext()
        {
        }

        public DbSet<Manager> Managers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SEED
            var course = new Course()
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = "JavaScript",
                Duration = "6",
                Description = "",
                Price = 800

            };

            var teacher = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Petar",
                SecondName = "Petrov",
                LastName = "Georgiev",
                Education = "Higher",
                Email = "georgiev@gmail.com",
                Experience = 6,
                Position = "Teacher",
                Salary = 2000,
                Year = 21,
                PhoneNumber = "202-555-0108"
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
            modelBuilder.Entity<Teacher>().HasData(teacher);
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