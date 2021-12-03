using Date;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class CourseService
    {
        private const string IMAGE_FOLDET_NAME = "/ImageFromCourses";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public CourseService(ApplicationDbContext dbContext ,
                             IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        // Show all courses
        public IEnumerable<CourseViewModel> GetAllCourses()
        {
            IEnumerable<CourseViewModel> courses = this.dbContext.Courses
                .Select(courses => new CourseViewModel
                {
                    Id = courses.Id,
                    CourseName = courses.CourseName,
                    ImageFile = courses.ImageFile,
                    ImageName = courses.ImageName,
                    Description = courses.Description,
                    Duration = courses.Duration,
                    Price = courses.Price
                }).ToList();

            return courses;
        }

        // Get course by ID
        public CourseViewModel GetCourseById(string id)
        {
            CourseViewModel model = this.dbContext.Courses
                .Select(course => new CourseViewModel
                { 
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Duration = course.Duration,
                    ImageFile = course.ImageFile,
                    ImageName = course.ImageName,
                    Price = course.Price
                }).SingleOrDefault(course => course.Id == id);

            return model;
        }

        // Get course by name
        public Course GetCourseByName(string courseName)
        {
            Course courseDb = this.dbContext.Courses
                .SingleOrDefault(x => x.CourseName == courseName);

            return courseDb;
        }

        // Create course
        public async Task CreateCourse(CourseViewModel model)
        {
            Course course = new Course();

            course.Id = model.Id;
            course.CourseName = model.CourseName;
            course.Description = model.Description;
            course.Duration = model.Duration;
            course.ImageFile = model.ImageFile;
            course.ImageName = model.ImageName;
            course.Price = model.Price;

            if (model.ImageFile != null)
            {
                await SetImage(course);
            }

            await this.dbContext.Courses.AddAsync(course);
            await this.dbContext.SaveChangesAsync();
        }

        private async Task SetImage(Course course)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(course.ImageFile.FileName);
            string exension = Path.GetExtension(course.ImageFile.FileName);
            course.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDET_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await course.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
