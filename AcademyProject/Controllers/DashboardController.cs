using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;

namespace AcademyProject.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        public DashboardController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            UserViewModel user = _userService.GetDetailsById(ViewBag.userId = _userManager.GetUserId(HttpContext.User));
            return View(user);
        }

        public IActionResult Courses()
        {
            IEnumerable<UserViewModel> courses = _userService.GetAllCourses();

            return PartialView("_CourseDashboardPartial", courses);
        }
        public IActionResult Posts()
        {
            IEnumerable<UserViewModel> posts = _userService.GetAllPosts();

            return PartialView("_PostDashboardPartial", posts);
        }
    }
}