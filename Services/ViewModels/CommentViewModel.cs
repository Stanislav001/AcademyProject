using System.Collections.Generic;

using Models.Models;

namespace Services.ViewModels
{
    public class CommentViewModel
    {
        public string CommentId { get; set; }
        public string Context { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<User> Users { get; set; }
    }
}