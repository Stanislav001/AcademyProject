using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ViewModels
{
    public class TeacherViewModel
    {
        public string Id { get; set; }
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
        public List<CourseViewModel> Courses { get; set; }
        public List<StudentViewModel> Students { get; set; }
        public ManagerViewModel Manager { get; set; }
    }
}
