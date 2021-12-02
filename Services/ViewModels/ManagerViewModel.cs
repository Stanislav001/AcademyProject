using System.Collections.Generic;

namespace Services.ViewModels
{
    public class ManagerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Male { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public double Salary { get; set; }
        public List<CourseViewModel> Courses { get; set; }
    }
}
