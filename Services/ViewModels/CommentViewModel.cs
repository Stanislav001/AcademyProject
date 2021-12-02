using System;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class CommentViewModel
    {
        public string Context { get; set; }
        public List<PostViewModel> Posts { get; set; }

        public List<UserViewModel> Users { get; set; }
    }
}
