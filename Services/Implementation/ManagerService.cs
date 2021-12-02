using Date;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class ManagerService
    {
        public ApplicationDbContext dbContext { get; set; }

        public ManagerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all courses
        public async Task<List<CourseViewModel>> GetAllCourses()
        {
            var courses = dbContext.Courses.Select(x => new CourseViewModel
            {
                Id = x.Id,
                Context = x.Context,
                Duration = x.Duration,
                Price = x.Price,
                Title = x.Title
            }).ToListAsync();

            return await courses;
        }

        // Add course
        public async Task AddCourse(CourseViewModel course)
        {
            var courseDb = new Course();

            courseDb.Id = Guid.NewGuid().ToString();
            courseDb.Title = course.Title;
            courseDb.Context = course.Context;
            courseDb.Duration = course.Duration;
            courseDb.Price = course.Price;

            if (course.Title != null)
            {
                dbContext.Add(courseDb);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Eror!");
            }
        }
    }
}
