using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class TeacherController : Controller
    {
        public ITeacherService teacherService { get; set; }

        public TeacherController(ITeacherService service)
        {
            teacherService = service;
        }


        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TeacherViewModel> teachers = this.teacherService.GetAll();

            return this.View(teachers);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            TeacherViewModel teacher = teacherService.GetDetailsById(id);

            bool isTeacherNull = teacher == null;
            if (isTeacherNull)
            {
                return this.RedirectToAction("index");
            }
            return this.View(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> GetteacherById(string id)
        {
            TeacherViewModel result = teacherService.GetDetailsById(id);
            return View("_CoursePartial", result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<TeacherViewModel> teachers = this.teacherService.GetByName();

            bool areTeachersEmpty = teachers.Count() == 0;
            if (areTeachersEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.teachers = teachers;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Teacher teacherDb = this.teacherService.GetByModelName(model.FirstName);

            bool isTeacherAlreadyInDb = teacherDb != null;
            if (isTeacherAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }
            await this.teacherService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

    }
}
