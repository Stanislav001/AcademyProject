using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> GetAll();
        TeacherViewModel GetDetailsById(string id);
        Task CreateAsync(TeacherViewModel model);
        TeacherViewModel UpdateById(string id);
        Task UpdateAsync(TeacherViewModel model);
        Task DeleteAsync(string id);
    }
}
