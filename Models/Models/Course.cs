using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Models.Base;

namespace Models.Models
{
   public class Course: BaseModel
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<Teacher> Teachers { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
    }
}