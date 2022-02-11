using System.Collections.Generic;

namespace Services.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}