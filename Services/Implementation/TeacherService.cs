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
    public class TeacherService : BaseService, ITeacherService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForTeacher";
        private readonly IWebHostEnvironment hostEnvironment;

        public TeacherService(ApplicationDbContext dbContext , IWebHostEnvironment hostEnvironment, IMapper mapper) : base(dbContext, mapper)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<TeacherViewModel> GetAll()
        {
            IEnumerable<TeacherViewModel> teachers = this.DbContext.Teachers
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
                    Year = teachers.Year
                }).ToList();

            return teachers;
        }

        public TeacherViewModel GetDetailsById(string id)
        {
            TeacherViewModel teacher = this.DbContext.Teachers
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
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName
                }).SingleOrDefault(teacher => teacher.Id == id);

            return teacher;
        }


        public IEnumerable<TeacherViewModel> GetByName()
        {
            IEnumerable<TeacherViewModel> teacher = this.DbContext.Teachers
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
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName
                }).OrderBy(teacher => teacher.FirstName).ToList();

            return teacher;
        }

        public Teacher GetByModelName(string modelName)
        {
            Teacher teacherDb = this.DbContext.Teachers
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
            teacher.Salary = model.Salary;
            teacher.Email = model.Email;
            teacher.Education = model.Education;
            teacher.Experience = model.Experience;
            teacher.ImageName = model.ImageName;
            teacher.ImageFile = model.ImageFile;
            teacher.PhoneNumber = model.PhoneNumber;

            if (model.ImageFile != null)
            {
                await SetImage(teacher);
            }

            await this.DbContext.Teachers.AddAsync(teacher);
            await this.DbContext.SaveChangesAsync();
        }

        public TeacherViewModel UpdateById(string id)
        {
            TeacherViewModel teacher = this.DbContext.Teachers
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
                    Year = teacher.Year

                }).SingleOrDefault(teacher => teacher.Id == id);

            return teacher;
        }

        public async Task UpdateAsync(TeacherViewModel model)
        {
            Teacher teacher = this.DbContext.Teachers.Find(model.Id);

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

            if (teacher.ImageName != null)
            {
                model.ImageName = teacher.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(teacher);
            }

            this.DbContext.Teachers.Update(teacher);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Teacher teacher = new Teacher();
            teacher = this.DbContext.Teachers.Find(id);

            bool isTeacherNull = teacher == null;
            if (isTeacherNull)
            {
                return;
            }

            this.DbContext.Teachers.Remove(teacher);
            await this.DbContext.SaveChangesAsync();
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