using AcademyProject.Models.Courses.BindingModels;
using AcademyProject.Models.Courses.ViewModels;
using AcademyProject.Services.Interfaces;
using Date;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyProject.Services.Implementation
{
    public class CourseService : ICoursesService
    {
        private const string IMAGE_FOLDET_NAME = "/ImageFromCourses";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public CourseService(ApplicationDbContext dbContext,
            IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<CourseViewModel> GetAll(string id)
        {
            IEnumerable<CourseViewModel> course = this.dbContext.Courses
               .Select(course => new CourseViewModel
               {
                   CourseId = course.Id,
                   CourseName = course.CourseName,
                   Duration = course.Duration,
                   Description = course.Description,
                   ImageName = course.ImageName,
                   ImageFile = course.ImageFile,
                   Price = course.Price
               })
               .ToList();
            return course;
        }

        public CourseViewModel GetDetailsById(string id)
        {
            CourseViewModel course = this.dbContext.Courses
                .Select(course => new CourseViewModel
                {
                    CourseId = course.Id,
                    CourseName = course.CourseName,
                    Duration = course.Duration,
                    Description = course.Description,
                    ImageName = course.ImageName,
                    ImageFile = course.ImageFile,
                    Price = course.Price
                }).SingleOrDefault(x => x.CourseId == id);
            return course;
        }

        public Course GetByModelName(string modelName)
        {
            Course courseDb = this.dbContext.Courses
                .SingleOrDefault(x => x.CourseName == modelName);
            return courseDb;
        }

        public async Task CreateAsync(CreateCoursesBindingModel model)
        {
            Course course = new Course();

            course.CourseName = model.CourseName;
            course.Description = model.Description;
            course.Duration = model.Duration;
            course.Price = model.Price;
            course.ImageFile = model.ImageFile;
            course.ImageName = model.ImageName;

            if (model.ImageFile != null)
            {
                await SetImage(course);
            }

            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
        }

        public UpdateCoursesBindingModel UpdateById(string id)
        {
            UpdateCoursesBindingModel course = this.dbContext.Courses
                .Select(course => new UpdateCoursesBindingModel
                {
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Duration = course.Duration,
                    ImageFile = course.ImageFile,
                    ImageName = course.ImageName,
                    Price = course.Price,
                    Id = course.Id
                })
                .SingleOrDefault(m => m.Id == id);

            return course;
        }

        public async Task UpdateAsync(UpdateCoursesBindingModel model)
        {
            Course course = this.dbContext.Courses
                .Find(model.Id);

            bool isCourseNull = course == null;
            if (isCourseNull)
            {
                return;
            }

            course.CourseName = model.CourseName;
            course.Duration = model.Duration;
            course.Price = model.Price;
            course.Description = model.Description;
            course.ImageFile = model.ImageFile;
            course.ImageName = model.ImageName;

            if (course.ImageName != null && model.ImageName == null)
            {
                model.ImageName = course.ImageName;
            }

            if (model.ImageFile != null)
            {
                await SetImage(course);
            }

            this.dbContext.Courses.Update(course);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Course course = new Course();
            course = this.dbContext.Courses
                   .Find(id);

            bool isCoursesNull = course == null;
            if (isCoursesNull)
            {
                return;
            }

            this.dbContext.Courses.Remove(course);
            await this.dbContext.SaveChangesAsync();
        }

        public bool CheckIfCourseExist(string id)
        {
            Course course = this.dbContext.Courses
                .Where(m => m.Id == id)
                .SingleOrDefault();

            bool isExist = course != null;

            return isExist;
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
