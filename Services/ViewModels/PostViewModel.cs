using System;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public List<CommentViewModel> Coments { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}
