using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService gradeService;
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;
        public GradeController(IGradeService gradeService , UserManager<User> userManager, IUserService userService)
        {
            this.gradeService = gradeService;
            this.userService = userService;
            this.userService = userService;
        }

        public async Task<IActionResult> GetAllGrades()
        {
            string id = this.userManager.GetUserId(this.User);

            var grades = await gradeService.GetAllGradesAsync(id);

            return View(grades);
        }

        public IActionResult Create()
        {
            this.ViewData["Users"] = userService.GetAllUsernames(userManager.GetUserId(this.User));
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await gradeService.CreateGradeAsync(
                model.CourseId,
                model.StudentId,
                userManager.GetUserId(this.User));

            return RedirectToAction("GetAllReceiveGrades");
        }
    }
}
