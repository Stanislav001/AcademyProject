using Microsoft.AspNetCore.Http;

namespace AcademyProject.Models.Courses.ViewModels
{
    public class GetAllCoursesViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
