using System.Collections.Generic;

using Models.Base;

namespace Models.Models
{
    public class Comment: BaseModel
    {
        public string Context { get; set; }
        public List<Post> Posts { get; set; }

        public List<User> Users { get; set; }

    }
}