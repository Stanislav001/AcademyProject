using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementation
{
    public class StudentService : BaseService, IStudentService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForStudent";
        private readonly IWebHostEnvironment hostEnvironment;

        public StudentService(ApplicationDbContext dbContext , IWebHostEnvironment hostEnvironment, IMapper mapper) : base(dbContext, mapper)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<StudentViewModel> GetAll()
        {
            IEnumerable<StudentViewModel> students = this.DbContext.Students
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    City = student.City,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    ImageFile = student.ImageFile,
                    ImageName = student.ImageName,
                    Year = student.Year,
                    StudentNumber = student.StudentNumber,
                    CoursesNumber = student.CoursesNumber
                }).ToList();

            return students;
        }

        public StudentViewModel GetDetailsById(string id)
        {
            StudentViewModel student = this.DbContext.Students
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    City = student.City,
                    Year = student.Year,
                    StudentNumber = student.StudentNumber,
                    ImageFile = student.ImageFile,
                    ImageName = student.ImageName
                }).SingleOrDefault(student => student.Id == id);

            return student;
        }

        public IEnumerable<StudentViewModel> GetByName()
        {
            IEnumerable<StudentViewModel> student = this.DbContext.Students
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    City = student.City,
                    Year = student.Year,
                    ImageFile = student.ImageFile,
                    ImageName = student.ImageName
                }).OrderBy(student => student.FirstName).ToList();

            return student;
        }
        public Student GetByModelName(string modelName)
        {
            Student studentDb = this.DbContext.Students
                .SingleOrDefault(student => student.FirstName == modelName);

            return studentDb;
        }

        public async Task CreateAsync(StudentViewModel model)
        {
            Student student = new Student();

            student.Id = Guid.NewGuid().ToString();
            student.FirstName = model.FirstName;
            student.SecondName = model.SecondName;
            student.LastName = model.LastName;
            student.Year = model.Year;
            student.Email = model.Email;
            student.PhoneNumber = model.PhoneNumber;
            student.Year = model.Year;
            student.City = model.City;
            student.ImageName = model.ImageName;
            student.ImageFile = model.ImageFile;

            if (model.ImageFile != null)
            {
                await SetImage(student);
            }

            await this.DbContext.Students.AddAsync(student);
            await this.DbContext.SaveChangesAsync();
        }

        public StudentViewModel UpdateById(string id)
        {
            StudentViewModel student = this.DbContext.Students
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    PhoneNumber = student.PhoneNumber,
                    Year = student.Year,
                    City = student.City,
                    CoursesNumber = student.CoursesNumber,
                    Email = student.Email,
                    StudentNumber = student.StudentNumber,
                    ImageFile = student.ImageFile,
                    ImageName = student.ImageName
                }).SingleOrDefault(student => student.Id == id);

            return student;
        }

        public async Task UpdateAsync(StudentViewModel model)
        {
            Student student = this.DbContext.Students.Find(model.Id);

            bool isStudentNull = student == null;
            if (isStudentNull)
            {
                return;
            }

            student.FirstName = model.FirstName;
            student.SecondName = model.SecondName;
            student.LastName = model.LastName;
            student.PhoneNumber = model.PhoneNumber;
            student.Year = model.Year;
            student.StudentNumber = model.StudentNumber;
            student.Email = model.Email;
            student.ImageName = model.ImageName;
            student.ImageFile = model.ImageFile;

            if (student.ImageName != null)
            {
                model.ImageName = student.ImageName;
            }
            if (model.ImageFile != null)
            {
                await SetImage(student);
            }

            this.DbContext.Students.Update(student);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            Student student = new Student();
            student = this.DbContext.Students.Find(id);

            bool isStudentNull = student == null;
            if (isStudentNull)
            {
                return;
            }

            this.DbContext.Students.Remove(student);
            await this.DbContext.SaveChangesAsync();
        }

        private async Task SetImage(Student course)
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

        ////TODO:
        // Course by Student
        public async Task<bool> AddCourseByStudentAsync(string courseID, string studentId)
        {
            CourseStudent model = new CourseStudent
            {
                CourseId = courseID,
                StudentId = studentId
            };

            await this.DbContext.CourseStudent.AddAsync(model);
            await this.DbContext.SaveChangesAsync();

            return true;
        }
        
    }
}