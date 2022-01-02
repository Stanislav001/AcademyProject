using System.Collections.Generic;

using Models.Base;

namespace Models.Models
{
   public class Post: BaseModel
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public List<Comment> Coments { get; set; }
        public List<User> Users { get; set; }

    }
}