using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class UserController : Controller
    {
        public IUserService userService { get; set; }

        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            UserViewModel user = userService.GetDetailsById(id);

            bool isUserNull = user == null;
            if (isUserNull)
            {
                return this.RedirectToAction("index");
            }
            return this.View(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            UserViewModel result = userService.GetDetailsById(id);
            return View("/Dashboard/Index", result); 
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            UserViewModel user = this.userService.UpdateById(id);

            return this.View(user);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UserViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.userService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }
    }
}
