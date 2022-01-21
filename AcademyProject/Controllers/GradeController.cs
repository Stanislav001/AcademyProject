using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class GradeController : Controller
    {
        public IGradeService gradeService { get; set; }

        public GradeController(IGradeService service)
        {
            gradeService = service;
        }

        // TODO: Refactoring
        public IActionResult Create()
        {
            GradeViewModel gradeModel = new GradeViewModel();

            var courseId = gradeModel.CourseId;
            var studentId = gradeModel.StudentId;
            var gradeContext = gradeModel.StudentGrade;

            this.ViewData["Users"] = gradeService.CreateGradeAsync(courseId, studentId, gradeContext);
            return this.View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(GradeViewModel gradeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(gradeViewModel);
            }

            await gradeService.CreateGradeAsync(
                gradeViewModel.CourseId,
                gradeViewModel.StudentId,
                gradeViewModel.StudentGrade);

            return RedirectToAction("GetAllReceivedMessages");
        }
    }
}
