using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

using Models.Base;

namespace Models.Models
{
    public class Teacher : BaseModel
    {
        public string FirstName { get; set; }
        [AllowNull]
        public string SecondName { get; set; }
        [AllowNull]
        public string LastName { get; set; }
        [AllowNull]
        public string Education { get; set; }
        [AllowNull]
        public int Year { get; set; }
        [AllowNull]
        public int Experience { get; set; }
        [AllowNull]
        public string Position { get; set; }
        [AllowNull]
        public decimal Salary { get; set; }
        [AllowNull]
        public string PhoneNumber { get; set; }
        [AllowNull]
        public string Email { get; set; }
        [AllowNull]
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<Course> Courses { get; set; }
        public List<Student> Students { get; set; }
    }
}