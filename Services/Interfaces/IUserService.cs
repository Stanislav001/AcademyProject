using System.Threading.Tasks;
using System.Collections.Generic;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllCourses();
        UserViewModel GetDetailsById(string id);
        UserViewModel UpdateById(string id);
        Task UpdateAsync(UserViewModel model);
        public IEnumerable<User> GetAllUsernames(string userId);
        public IEnumerable<UserViewModel> GetAllPosts();
    }
}