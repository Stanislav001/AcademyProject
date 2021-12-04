using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> GetAllCourses();
        CourseViewModel GetCourseById(string id);
        IEnumerable<CourseViewModel> GetCourseByName();
        Task CreateCourse(CourseViewModel model);

    }
}
