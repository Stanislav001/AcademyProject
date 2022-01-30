using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


using Models.Base;

namespace Models.Models
{
    public class Student : BaseModel
    {
        public string StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CoursesNumber { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<Teacher> Teachers { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
        public Manager Manager { get; set; }
        public List<Grade> Grades { get; set; }
    }
}