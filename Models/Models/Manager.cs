using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Manager: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Male { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public double Salary { get; set; }
        public List<Course> Courses { get; set; }

    }
}
