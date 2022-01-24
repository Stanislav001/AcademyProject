using System.Collections.Generic;

namespace Services.ViewModels
{
    public class PostViewModel
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string UserId { get; set; }
        public List<UserViewModel> Users { get; set; }
    }
}