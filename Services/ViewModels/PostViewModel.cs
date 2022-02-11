using System;
using System.Collections.Generic;

using Models.Models;

namespace Services.ViewModels
{
    public class PostViewModel
    {
        public string PostId { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public UserViewModel User { get; set; }
        public string CommentContext { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}