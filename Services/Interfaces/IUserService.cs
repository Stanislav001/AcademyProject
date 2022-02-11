using System.Collections.Generic;
using System.Threading.Tasks;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllCourses();
        IEnumerable<UserViewModel> GetAllStudents();
        UserViewModel GetDetailsById(string id);
        UserViewModel UpdateById(string id);
        Task UpdateAsync(UserViewModel model);
        public IEnumerable<User> GetAllUsernames(string userId);
    }
}