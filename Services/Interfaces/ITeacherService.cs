using System.Threading.Tasks;
using System.Collections.Generic;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> GetAll();
        TeacherViewModel GetDetailsById(string id);
        IEnumerable<TeacherViewModel> GetByName();
        Teacher GetByModelName(string modelName);
        Task CreateAsync(TeacherViewModel model);
        TeacherViewModel UpdateById(string id);
        Task UpdateAsync(TeacherViewModel model);
        Task DeleteAsync(string id);
    }
}