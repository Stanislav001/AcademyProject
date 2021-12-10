using Date;
using Microsoft.AspNetCore.Hosting;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class TeacherService : ITeacherService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForTeacher";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public TeacherService(ApplicationDbContext dbContext , IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<TeacherViewModel> GetAll()
        {
            IEnumerable<TeacherViewModel> teachers = this.dbContext.Teachers
                .Select(teachers => new TeacherViewModel
                {
                    Id = teachers.Id,
                    FirstName = teachers.FirstName,
                    SecondName = teachers.SecondName,
                    LastName = teachers.LastName,
                    Education = teachers.Education,
                    Email = teachers.Email,
                    Experience = teachers.Experience,
                    ImageFile = teachers.ImageFile,
                    ImageName = teachers.ImageName,
                    PhoneNumber = teachers.PhoneNumber,
                    Position = teachers.Position,
                    Salary = teachers.Salary,
                    Year = teachers.Year,
                    TeacherNumber = teachers.TeacherNumber
                }).ToList();

            return teachers;
        }

        public TeacherViewModel GetDetailsById(string id)
        {
            TeacherViewModel teacher = this.dbContext.Teachers
                .Select(teacher => new TeacherViewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    SecondName = teacher.SecondName,
                    LastName = teacher.LastName,
                    Education = teacher.Education,
                    Email = teacher.Email,
                    Experience = teacher.Experience,
                    PhoneNumber = teacher.PhoneNumber,
                    Position = teacher.Position,
                    Salary = teacher.Salary,
                    TeacherNumber = teacher.TeacherNumber,
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName
                }).SingleOrDefault(teacher => teacher.Id == id);

            return teacher;
        }


        public IEnumerable<TeacherViewModel> GetByName()
        {
            IEnumerable<TeacherViewModel> teacher = this.dbContext.Teachers
                .Select(teacher => new TeacherViewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    SecondName = teacher.SecondName,
                    LastName = teacher.LastName,
                    Education = teacher.Education,
                    Email = teacher.Email,
                    Experience = teacher.Experience,
                    PhoneNumber = teacher.PhoneNumber,
                    Position = teacher.Position,
                    Salary = teacher.Salary,
                    Year = teacher.Year,
                    TeacherNumber = teacher.TeacherNumber,
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName
                }).OrderBy(teacher => teacher.FirstName).ToList();

            return teacher;
        }

        public Teacher GetByModelName(string modelName)
        {
            Teacher teacherDb = this.dbContext.Teachers
                .SingleOrDefault(teacher => teacher.FirstName == modelName);

            return teacherDb;
        }

        public async Task CreateAsync(TeacherViewModel model)
        {
            Teacher teacher = new Teacher();

            teacher.Id = Guid.NewGuid().ToString();
            teacher.Year = model.Year;
            teacher.FirstName = model.FirstName;
            teacher.SecondName = model.SecondName;
            teacher.LastName = model.LastName;
            teacher.TeacherNumber = model.TeacherNumber;
            teacher.Salary = model.Salary;
            teacher.Email = model.Email;
            teacher.Education = model.Education;
            teacher.Experience = model.Experience;
            teacher.ImageName = model.ImageName;
            teacher.ImageFile = model.ImageFile;

            if (model.ImageFile != null)
            {
                await SetImage(teacher);
            }

            await this.dbContext.Teachers.AddAsync(teacher);
            await this.dbContext.SaveChangesAsync();
        }

        public TeacherViewModel UpdateById(string id)
        {
            TeacherViewModel teacher = this.dbContext.Teachers
                .Select(teacher => new TeacherViewModel
                {
                    Id = teacher.Id,
                    Education = teacher.Education,
                    Email = teacher.Email,
                    Experience = teacher.Experience,
                    FirstName = teacher.FirstName,
                    SecondName = teacher.SecondName,
                    LastName = teacher.LastName,
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName,
                    PhoneNumber = teacher.PhoneNumber,
                    Position = teacher.Position,
                    Salary = teacher.Salary,
                    Year = teacher.Year,
                    TeacherNumber = teacher.TeacherNumber

                }).SingleOrDefault(teacher => teacher.Id == id);

            return teacher;
        }

        public async Task UpdateAsync(TeacherViewModel model)
        {
            Teacher teacher = this.dbContext.Teachers.Find(model.Id);

            bool isTeacherNull = teacher == null;
            if (isTeacherNull)
            {
                return;
            }

            teacher.FirstName = model.FirstName;
            teacher.SecondName = model.SecondName;
            teacher.LastName = model.LastName;
            teacher.Salary = model.Salary;
            teacher.Education = model.Education;
            teacher.Email = model.Email;
            teacher.Experience = model.Experience;
            teacher.ImageFile = model.ImageFile;
            teacher.ImageName = model.ImageName;
            teacher.PhoneNumber = model.PhoneNumber;
            teacher.TeacherNumber = model.TeacherNumber;

            if (teacher.ImageName != null && teacher.ImageName == null)
            {
                model.ImageName = teacher.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(teacher);
            }

            this.dbContext.Teachers.Update(teacher);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Teacher teacher = new Teacher();
            teacher = this.dbContext.Teachers.Find(id);

            bool isTeacherNull = teacher == null;
            if (isTeacherNull)
            {
                return;
            }

            this.dbContext.Teachers.Remove(teacher);
            await this.dbContext.SaveChangesAsync();
        }


        private async Task SetImage(Teacher teacher)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(teacher.ImageFile.FileName);
            string exension = Path.GetExtension(teacher.ImageFile.FileName);
            teacher.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await teacher.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
