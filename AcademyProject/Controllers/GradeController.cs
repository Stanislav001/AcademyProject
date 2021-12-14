using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;
        public GradeController(IGradeService gradeService)
        {
            this.gradeService = gradeService;
        }


    }
}
