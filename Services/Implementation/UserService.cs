using System;
using System.IO;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Implementation
{
    public class UserService : BaseService, IUserService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForUser";
        private readonly IWebHostEnvironment hostEnvironment;

        public UserService(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment, IMapper mapper)
                           : base(dbContext, mapper)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public UserViewModel GetDetailsById(string id)
        {
            UserViewModel user = this.DbContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Country = user.Country,
                    Email = user.Email,
                    UserName = user.UserName,
                    ImageFile = user.ImageFile,
                    ImageName = user.ImageName,
                    Profession = user.Profession,
                }).SingleOrDefault(user => user.Id == id);

            return user;
        }

        public UserViewModel UpdateById(string id)
        {
            UserViewModel user = this.DbContext.Users
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Profession = user.Profession,
                    Country = user.Country,
                    UserName = user.UserName,
                    ImageFile = user.ImageFile,
                    ImageName = user.ImageName,
                }).SingleOrDefault(user => user.Id == id);

            return user;
        }

        public async Task UpdateAsync(UserViewModel model)
        {
            User user = this.DbContext.Users.Find(model.Id);

            bool isUserNull = user == null;
            if (isUserNull)
            {
                return;
            }

            user.Id = model.Id;
            user.Email = model.Email;
            user.Profession = model.Profession;
            user.Country = model.Country;
            user.UserName = model.UserName;
            user.ImageFile = model.ImageFile;
            user.ImageName = model.ImageName;
            

            if (user.ImageName != null)
            {
                model.ImageName = user.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(user);
            }

            this.DbContext.Users.Update(user);
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SetImage(User user)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(user.ImageFile.FileName);
            string exension = Path.GetExtension(user.ImageFile.FileName);
            user.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await user.ImageFile.CopyToAsync(fileStream);
            }
        }

        public IEnumerable<User> GetAllUsernames(string userId)
        {
            IEnumerable<User> user = this.DbContext.Users.Where(u => u.Id == userId).ToList();

            return user;
        }

        public User GetUserByUserName(string userName)
        {
            User user = this.DbContext.Users
              .Select(user => new User
              {
                  Id = user.Id,
                  Country = user.Country,
                  Email = user.Email,
                  UserName = user.UserName,
                  ImageFile = user.ImageFile,
                  ImageName = user.ImageName,
                  Profession = user.Profession,
              }).SingleOrDefault(user => user.UserName == userName);

            return user;
        }

        // Return all Students
        public IEnumerable<UserViewModel> GetAllStudents()
        {
            IEnumerable<UserViewModel> students = this.DbContext.Students
                .Select(student => new UserViewModel
                {
                    Id = student.Id,
                    StudentFirstName = student.FirstName,
                    StudentSecondName = student.SecondName,
                    StudentLastName = student.LastName,
                    StudentPhoneNumber = student.PhoneNumber,
                    StudentEmail = student.Email,
                }).ToList();

            return students;
        }

        // Return all courses
        public IEnumerable<UserViewModel> GetAllCourses()
        {
            IEnumerable<UserViewModel> courses = this.DbContext.Courses
                .Select(t => new UserViewModel
                {
                    CourseName = t.CourseName,
                    CoursePrice = t.Price,
                    CourseDuration = t.Duration,
                }).ToList();

            return courses;
        }

        public IEnumerable<UserViewModel> GetAllPosts()
        {
            IEnumerable<UserViewModel> posts = this.DbContext.Posts
                .Select(post => new UserViewModel
                {
                    PostTitle = post.UserName,
                    UserName = post.Context
                }).ToList();

            return posts;
        }
    }
}