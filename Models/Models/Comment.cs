using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class Comment: BaseModel
    {
        public string Context { get; set; }
        public List<Post> Posts { get; set; }

        public List<User> Users { get; set; }

    }
}
