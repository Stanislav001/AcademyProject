using Services.ViewModels;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> GetAll();
        StudentViewModel GetDetailsById(string id);
    }
}
