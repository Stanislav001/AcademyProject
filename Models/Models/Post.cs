using System.Collections.Generic;

using Models.Base;

namespace Models.Models
{
   public class Post: BaseModel
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public List<Comment> Coments { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public User User { get; set; }

    }
}