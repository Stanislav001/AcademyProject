using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyProject.Models.Courses.ViewModels
{
    public class DetailsCoursesViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
