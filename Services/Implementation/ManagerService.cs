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

namespace Services.Implementation
{
    public class ManagerService : BaseService , IManagerService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageForManager";
        private readonly IWebHostEnvironment hostEnvironment;

        public ManagerService(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment hostEnvironment) : base(dbContext, mapper)
        {
            this.hostEnvironment = hostEnvironment;
        }

        // Return all students
        public IEnumerable<StudentViewModel> GetAllStudents()
        {
            IEnumerable<StudentViewModel> students = this.DbContext.Students
                .Select(student => new StudentViewModel
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    City = student.City,
                    Email = student.Email,
                    ImageFile = student.ImageFile,
                    ImageName = student.ImageName,
                    Year = student.Year,
                    StudentNumber = student.StudentNumber,
                    CoursesNumber = student.CoursesNumber,
                    PhoneNumber = student.PhoneNumber,
                }).ToList();

            return students;
        }

        // Return all teachers
        public IEnumerable<TeacherViewModel> GetAllTeachers()
        {
            IEnumerable<TeacherViewModel> teachers = this.DbContext.Teachers
                .Select(teacher => new TeacherViewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    SecondName = teacher.SecondName, 
                    LastName = teacher.LastName,
                    Education = teacher.Education,
                    Email = teacher.Email,
                    Experience = teacher.Experience,
                    ImageFile = teacher.ImageFile,
                    ImageName = teacher.ImageName,
                    Year = teacher.Year,
                    PhoneNumber = teacher.PhoneNumber,
                    Position = teacher.Position,
                    Salary = teacher.Salary,
                }).ToList();

            return teachers;
        }
    }
}