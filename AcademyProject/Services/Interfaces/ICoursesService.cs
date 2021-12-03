using AcademyProject.Models.Courses.BindingModels;
using AcademyProject.Models.Courses.ViewModels;
using Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademyProject.Services.Interfaces
{
    public interface ICoursesService
    {
        IEnumerable<CourseViewModel> GetAll(string id);

        CourseViewModel GetDetailsById(string id);

        Course GetByModelName(string modelName);

        bool CheckIfCourseExist(string id);

        Task CreateAsync(CreateCoursesBindingModel model);

        UpdateCoursesBindingModel UpdateById(string id);

        Task UpdateAsync(UpdateCoursesBindingModel model);

        Task DeleteAsync(string id);
    }
}
