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

        public DbSet<Course> Courses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }

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

            var fail = new Grade()
            {
                Id = Guid.NewGuid().ToString(),
                StudentGrade =2
            };

            var average = new Grade()
            {
                Id = Guid.NewGuid().ToString(),
                StudentGrade = 3
            };

            var good = new Grade()
            {
                Id = Guid.NewGuid().ToString(),
                StudentGrade = 4
            };

            var veryGood = new Grade()
            {
                Id = Guid.NewGuid().ToString(),
                StudentGrade = 5
            };

            var excellent = new Grade()
            {
                Id = Guid.NewGuid().ToString(),
                StudentGrade = 6
            };

            var AdminRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin" };
            var UserRole = new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User" };

            modelBuilder.Entity<Course>().HasData(course);
            modelBuilder.Entity<Teacher>().HasData(teacher);
            modelBuilder.Entity<Student>().HasData(student);
            modelBuilder.Entity<IdentityRole>().HasData(AdminRole, UserRole);
            modelBuilder.Entity<Grade>().HasData(fail, average, good, veryGood, excellent);


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-4EJHC35;Database=Academy;Trusted_Connection=True");
        }
    }
}