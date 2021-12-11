using Microsoft.AspNetCore.Http;
using Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public List<Student> Students { get; set; }
        public Manager Manager { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
