using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademyProject.Models.Courses.BindingModels
{
    public class CreateCoursesBindingModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        [DisplayName("course")]
        public string CourseId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }

        public string ImageName { get; set; }

        [DisplayName("Upload image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
