using System.Collections.Generic;

using Services.ViewModels.RankingViewModels;

namespace Services.Interfaces
{
    public interface ITopCoursesService
    {
        public IEnumerable<TopCourseViewModel> GetAll();
    }
}