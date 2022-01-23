using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class GradeController : Controller
    {
        public IGradeService gradeService { get; set; }
        public IUserService userService { get; set; }

        public GradeController(IGradeService service)
        {
            gradeService = service;
        }

       
        /*
        [HttpPost]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await gradeService.CreateGradeAsync(
                model.CourseId,
                model.StudentId);

            return RedirectToAction("GetAllReceivedMessages");
        }
        */

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

        // TODO: Refactoring
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