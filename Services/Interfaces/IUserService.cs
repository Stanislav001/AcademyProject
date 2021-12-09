using Services.ViewModels;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel GetDetailsById(string id);
        UserViewModel UpdateById(string id);
        Task UpdateAsync(UserViewModel model);
    }
}
