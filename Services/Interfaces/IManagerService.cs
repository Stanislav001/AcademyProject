using Models.Models;
using Services.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IManagerService 
    {
        IEnumerable<ManagerViewModel> GetAll();
        ManagerViewModel GetDetailsById(string id);
        IEnumerable<ManagerViewModel> GetByFirstName();
        Manager GetByModelFirstName(string modelName);
        Task CreateAsync(ManagerViewModel model);
        ManagerViewModel UpdateById(string id);
        Task UpdateAsync(ManagerViewModel model);
        Task DeleteAsync(string id);
    }
}
