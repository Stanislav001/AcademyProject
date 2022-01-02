using System.Collections.Generic;
using System.Threading.Tasks;

using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel GetDetailsById(string id);
        UserViewModel UpdateById(string id);
        Task UpdateAsync(UserViewModel model);
        public IEnumerable<UserViewModel> GetAllUsernames(string userId);
    }
}