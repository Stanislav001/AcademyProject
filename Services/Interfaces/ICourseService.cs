using Models.Models;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> GetAll();
        CourseViewModel GetDetailsById(string id);
        IEnumerable<CourseViewModel> GetByName();
        Course GetByModelName(string modelName);
        Task CreateAsync(CourseViewModel model);
        CourseViewModel UpdateById(string id);
        Task UpdateAsync(CourseViewModel model);
        Task DeleteAsync(string id);


    }
}
