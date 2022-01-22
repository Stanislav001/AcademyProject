using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace AcademyProject.Controllers
{
    public class CourseController : Controller
    {
       public ICourseService courseService { get; set; }

        public CourseController(ICourseService service)
        {
            courseService = service;
        }

        // Show all courses
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<CourseViewModel> courses = this.courseService.GetAll();

            return this.View(courses);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            CourseViewModel course = courseService.GetDetailsById(id);

            bool isCourseNull = course == null;
            if (isCourseNull)
            {
                return this.RedirectToAction("index");
            }
            return this.View(course);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseById(string id)
        {
            CourseViewModel result =  courseService.GetDetailsById(id);

            return View(@"CoursePartials\_CoursePartial", result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<CourseViewModel> courses = this.courseService.GetByName();

            bool areCoursesEmpty = courses.Count() == 0;
            if (areCoursesEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.courses = courses;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Course courseDb = this.courseService.GetByModelName(model.CourseName);

            bool isCourseAlreadyInDb = courseDb != null;
            if (isCourseAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }
            await this.courseService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            CourseViewModel course = this.courseService.UpdateById(id);

            return this.View(course);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(CourseViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.courseService.UpdateAsync(model);

            return this.RedirectToAction("_UpdateUserPartial");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.courseService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}