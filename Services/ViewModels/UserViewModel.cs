using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class UserViewModel : IdentityUser
    {
        public string Country { get; set; }
        public List<CourseViewModel> Courses { get; set; }
        public List<PostViewModel> Posts { get; set; }
        public List<CommentViewModel> Coments { get; set; }
    }
}
