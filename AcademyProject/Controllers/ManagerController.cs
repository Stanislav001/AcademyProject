using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace AcademyProject.Controllers
{
    public class ManagerController : Controller
    {
        public IManagerService managerService { get; set; }
        public ManagerController(IManagerService service)
        {
            managerService = service;
        }

    }
}