using Models.Models;
using System.Collections.Generic;

namespace Services.ViewModels
{
    public class CommentViewModel
    {
        public string Id { get; set; }
        public string Context { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public List<User> Users { get; set; }
    }
}