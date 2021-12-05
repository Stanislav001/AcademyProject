using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class ManagerController : Controller
    {
        public IManagerService managerService { get; set; }
        public ManagerController(IManagerService service)
        {
            managerService = service;
        }

        // Show all courses
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<ManagerViewModel> courses = this.managerService.GetAll();

            return this.View(courses);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            ManagerViewModel manager = managerService.GetDetailsById(id);

            bool isManagerNull = manager == null;
            if (isManagerNull)
            {
                return this.RedirectToAction("index");
            }
            return this.View(manager);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<ManagerViewModel> managers = this.managerService.GetByFirstName();

            bool areManagersEmpty = managers.Count() == 0;
            if (areManagersEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.managers = managers;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManagerViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Manager managerDb = this.managerService.GetByModelFirstName(model.FirstName);

            bool isManagerAlreadyInDb = managerDb != null;
            if (isManagerAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }
            await this.managerService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            ManagerViewModel manager = this.managerService.UpdateById(id);

            return this.View(manager);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(ManagerViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.managerService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.managerService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
