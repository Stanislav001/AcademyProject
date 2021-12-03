using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class User : IdentityUser
    {
        public string Country { get; set; }
        public List<Course> Courses { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Coments { get; set; }

    }
}
