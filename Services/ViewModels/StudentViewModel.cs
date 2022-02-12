using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ViewModels
{
    public class StudentViewModel
    {
        public string Id { get; set; }
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
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}