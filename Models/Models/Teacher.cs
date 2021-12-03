
using Microsoft.AspNetCore.Http;
using Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Teacher : BaseModel
    {
        public string TeacherNumber { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Education { get; set; }
        public int Year { get; set; }
        public int Experience { get; set; }
        public int CoursesNumber { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
        public Manager Manager { get; set; }
    }
}
