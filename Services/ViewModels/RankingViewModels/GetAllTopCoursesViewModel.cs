using System.Collections.Generic;

namespace Services.ViewModels.RankingViewModels
{
    public class GetAllTopCoursesViewModel
    {
        public IEnumerable<TopCourseViewModel> Courses { get; set; }
    }
}