using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using Models.Constants.NotificationsConstants;

namespace AcademyProject.Controllers
{
    public class CourseController : Controller
    {
        private ICoursesUserService coursesUserService;
        private ISaveCourseUserService saveCourseUserService;
        private UserManager<User> userManager;
        private ICourseService courseService { get; set; }
        public IStudentService studentService { get; set; }
        public CourseController(ICourseService service, UserManager<User> userManager, ICoursesUserService coursesUserService, ISaveCourseUserService saveCourseUserService)
        {
            this.courseService = service;
            this.userManager = userManager;
            this.coursesUserService = coursesUserService;
            this.saveCourseUserService = saveCourseUserService;
        }

        // Show all courses
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            IEnumerable<CourseViewModel> course = this.courseService.GetAll(currentUser.Id);

            CoursesViewModel coursesViewModel = new CoursesViewModel();

            coursesViewModel.Courses = course;

            return this.View(coursesViewModel);
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

        [Authorize]
        public async Task<IActionResult> Enroll(string id)
        {
            User currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyVoted = await this.coursesUserService.EnrollUserToVoteAsync(currentUser.Id, id);

            if (isSuccessfullyVoted)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_VOTING;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_VOTED;
            }

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Disenroll(string id)
        {
            User currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyVoted = await this.coursesUserService.RemoveTheUserVoteAsync(currentUser.Id, id);
            if (isSuccessfullyVoted)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_UNVOTED;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_UNVOTED;
            }

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> StartCourse(string id)
        {
            User currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyStartedd = await this.saveCourseUserService.SaveStartedCourse(currentUser.Id, id);

            if (isSuccessfullyStartedd)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_START_COURSE;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_STARTED;
            }

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> unStarted(string id)
        {
            User currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyStarted = await this.saveCourseUserService.RemoveTheCourseAsync(currentUser.Id, id);
            if (isSuccessfullyStarted)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_START_COURSE;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_STARTED;
            }

            return this.RedirectToAction("index");
        }
    }
}