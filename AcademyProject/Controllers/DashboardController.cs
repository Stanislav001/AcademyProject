using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

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
    }
}
