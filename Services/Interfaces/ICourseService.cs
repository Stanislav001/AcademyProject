using System.Threading.Tasks;
using System.Collections.Generic;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> GetAll(string id);
        CourseViewModel GetDetailsById(string id);
        IEnumerable<CourseViewModel> GetByName();
        Course GetByModelName(string modelName);
        Task CreateAsync(CourseViewModel model);
        CourseViewModel UpdateById(string id);
        Task UpdateAsync(CourseViewModel model);
        Task DeleteAsync(string id);
    }
}