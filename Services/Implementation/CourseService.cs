using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using AutoMapper;

namespace Services.Implementation
{
    public class CourseService : BaseService , ICourseService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForCourse";
        private readonly IWebHostEnvironment hostEnvironment;

        public CourseService(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(dbContext, mapper)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<CourseViewModel> GetAll()
        {
            IEnumerable<CourseViewModel> courses = this.DbContext.Courses
                .Select(courses => new CourseViewModel
                {
                    Id = courses.Id,
                    CourseName = courses.CourseName,
                    ImageName = courses.ImageName,
                    Description = courses.Description,
                    Duration = courses.Duration,
                    ImageFile = courses.ImageFile,
                    Price = courses.Price
                }).ToList();

            return courses;
        }                                          

        public CourseViewModel GetDetailsById(string id)
        {
            CourseViewModel course = this.DbContext.Courses
                .Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Price = course.Price,
                    Duration = course.Duration,
                    ImageFile = course.ImageFile,
                    ImageName = course.ImageName
                }).SingleOrDefault(course => course.Id == id);

            return course;
        }

        public IEnumerable<CourseViewModel> GetByName()
        {
            IEnumerable<CourseViewModel> course = this.DbContext.Courses
                .Select(course => new CourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Duration = course.Duration,
                    Price = course.Price,
                    ImageFile = course.ImageFile,
                    ImageName = course.ImageName
                }).OrderBy(course => course.CourseName).ToList();

            return course;
        }

        public Course GetByModelName(string modelName)
        {
            Course courseDb = this.DbContext.Courses
                .SingleOrDefault(course => course.CourseName == modelName);

            return courseDb;
        }

        public async Task CreateAsync(CourseViewModel model)
        {
            Course course = new Course();

            course.Id = Guid.NewGuid().ToString();
            course.CourseName = model.CourseName;
            course.Price = model.Price;
            course.Description = model.Description;
            course.Duration = model.Duration;
            course.ImageName = model.ImageName;
            course.ImageFile = model.ImageFile;

            if (model.ImageFile !=null)
            {
                await SetImage(course);
            }

            await this.DbContext.Courses.AddAsync(course);
            await this.DbContext.SaveChangesAsync();
        }

        public CourseViewModel UpdateById(string id)
        {
            CourseViewModel course = this.DbContext.Courses
                .Select(course => new CourseViewModel
                {
                   Id = course.Id,
                   CourseName = course.CourseName,
                   Description = course.Description,
                   Price = course.Price,
                   Duration = course.Duration,
                   ImageFile = course.ImageFile,
                   ImageName = course.ImageName
                }).SingleOrDefault(course => course.Id == id);
            
            return course;
        }

        public async Task UpdateAsync(CourseViewModel model)
        {
            Course course = this.DbContext.Courses.Find(model.Id);

            bool isCourseNull = course == null;
            if (isCourseNull)
            {
                return;
            }

            course.CourseName = model.CourseName;
            course.Description = model.Description;
            course.Duration = model.Duration;
            course.Price = model.Price;
            course.ImageName = model.ImageName;
            course.ImageFile = model.ImageFile;

            if (course.ImageName != null && course.ImageName == null)
            {
                model.ImageName = course.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(course);
            }

            this.DbContext.Courses.Update(course);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Course course = new Course();
            course = this.DbContext.Courses.Find(id);

            bool isCourseNull = course == null;
            if (isCourseNull)
            {
                return;
            }

            this.DbContext.Courses.Remove(course);
            await this.DbContext.SaveChangesAsync();
        }

        public bool CheckIfCourseExist(string id)
        {
            Course course = this.DbContext.Courses
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
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await course.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}