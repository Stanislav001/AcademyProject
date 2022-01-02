using System.Collections.Generic;

using Models.Models;
using Services.ViewModels;

namespace Services.Interfaces
{
    public interface IManagerService 
    {
        IEnumerable<StudentViewModel> GetAllStudents();
        IEnumerable<TeacherViewModel> GetAllTeachers();
    }
}