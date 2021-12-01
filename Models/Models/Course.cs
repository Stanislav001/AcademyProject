using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
   public class Course: BaseModel
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public List<Manager> Managers { get; set; }
        public List<User> Users { get; set; }


    }
}
