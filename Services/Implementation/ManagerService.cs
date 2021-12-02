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

        // Create course
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
                Console.WriteLine("Eror. You can create course with null title");
            }
        }

        // Add Post
        public async Task AddPost (PostViewModel post)
        {
            var postDb = new Post();

            postDb.Id = Guid.NewGuid().ToString();
            postDb.Title = post.Title;
            postDb.Context = post.Context;

            if (post.Title!=null && post.Context!=null)
            {
                dbContext.Add(postDb);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Eror. You can create post with null title or null context");
            }
        }

        // Update course by id
        public CourseViewModel UpdateCourseById(string id)
        {
            CourseViewModel course = this.dbContext.Courses
                .Select(course => new CourseViewModel
                {
                    Title = course.Title,
                    Context = course.Context,
                    Duration = course.Duration,
                    Price = course.Price,
                    Id = course.Id
                })
                .SingleOrDefault(m => m.Id == id);

            return course;
        }

        public async Task UpdateCourse(CourseViewModel model)
        {
            Course course = this.dbContext.Courses
                .Find(model.Id);

            bool isCourseNull = course == null;
            if (isCourseNull)
            {
                return;
            }

            course.Title = model.Title;
            course.Duration = model.Duration;
            course.Price = model.Price;
            course.Context = model.Context;

            this.dbContext.Courses.Update(course);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
