using System.Collections.Generic;

using Models.Base;

namespace Models.Models
{
    public class Comment: BaseModel
    {
        public string Context { get; set; }
        public string PostId { get; set; }
        public List<Post> Post { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<User> Users { get; set; }

    }
}