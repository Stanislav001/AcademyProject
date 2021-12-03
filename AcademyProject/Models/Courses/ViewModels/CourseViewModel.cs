using Microsoft.AspNetCore.Http;

namespace AcademyProject.Models.Courses.ViewModels
{
    public class CourseViewModel
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string ImageName { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
