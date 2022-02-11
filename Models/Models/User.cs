using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class User : IdentityUser
    {
        public string Profession { get; set; }
        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string Country { get; set; }
        public List<Course> Courses { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Coments { get; set; }
        public List<CourseUser> UserCourse { get; set; }
    }
}