using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Services.ViewModels.RankingViewModels;

using Services.Interfaces;

namespace AcademyProject.Controllers
{
    public class TopCoursesController : Controller
    {
        private readonly ITopCoursesService topCoursesService;

        public TopCoursesController(ITopCoursesService topCoursesService)
        {
            this.topCoursesService = topCoursesService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<TopCourseViewModel> course = this.topCoursesService.GetAll();

            GetAllTopCoursesViewModel allCourses = new GetAllTopCoursesViewModel();
            allCourses.Courses = course;

            return this.View(allCourses);
        }
    }
}