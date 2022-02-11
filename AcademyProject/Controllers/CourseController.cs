using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace AcademyProject.Controllers
{
    public class CourseController : Controller
    {
        private ICoursesUserService coursesUserService;
        private UserManager<User> userManager;
        private ICourseService courseService { get; set; }
        public IStudentService studentService { get; set; }
        public CourseController(ICourseService service, UserManager<User> userManager, ICoursesUserService coursesUserService)
        {
            this.courseService = service;
            this.userManager = userManager;
            this.coursesUserService = coursesUserService;
        }

        // Show all courses
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            IEnumerable<CourseViewModel> course = this.courseService.GetAll(currentUser.Id);

            CoursesViewModel courseViewModel = new CoursesViewModel();

            courseViewModel.Courses = course;

            return this.View(courseViewModel);
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

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.courseService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }

        //TODO:
        // Course-Student 
        [HttpGet]
        public IActionResult AddMyCourse(string id)
        {
            this.ViewData["Courses"] = courseService.GetDetailsById(id);
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMyCourse(CourseStudent model)
        {
            await studentService.AddCourseByStudentAsync(
              model.CourseId,
              model.StudentId);

            return RedirectToAction("Index");
        }

    }
}