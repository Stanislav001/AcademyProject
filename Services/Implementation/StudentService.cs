using Date;
using Microsoft.AspNetCore.Hosting;
using Services.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Services.Implementation
{
    public class StudentService : IStudentService
    {
        public StudentService(ApplicationDbContext dbContext , IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }
        private const string IMAGE_FOLDER_NAME = "/ImageForStudent";
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public IEnumerable<StudentViewModel> GetAll()
        {
            IEnumerable<StudentViewModel> students = this.dbContext.Students
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

    }
}
