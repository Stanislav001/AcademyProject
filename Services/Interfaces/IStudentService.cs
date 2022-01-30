using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> GetAll();
        StudentViewModel GetDetailsById(string id);
        IEnumerable<StudentViewModel> GetByName();
        Student GetByModelName(string modelName);
        StudentViewModel UpdateById(string id);
        Task UpdateAsync(StudentViewModel model);
        Task CreateAsync(StudentViewModel model);
        Task DeleteAsync(string id);
        public Task<bool> AddCourseByStudentAsync(string courseID, string studentId);
    }
}