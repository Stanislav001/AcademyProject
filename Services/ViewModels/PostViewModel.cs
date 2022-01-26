using Models.Models;
using System.Collections.Generic;

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

        public string CommentId { get; set; }
        public string CommentContext { get; set; }
        public Post Post { get; set; }
    }
}