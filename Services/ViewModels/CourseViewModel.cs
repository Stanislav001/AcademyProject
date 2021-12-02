using System.Collections.Generic;

namespace Services.ViewModels
{
    public class CourseViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public List<ManagerViewModel> Managers { get; set; }
        public List<UserViewModel> Users { get; set; }

    }
}
