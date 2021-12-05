﻿using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Implementation;
using Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcademyProject.Controllers
{
    public class CourseController : Controller
    {
       public CourseService courseService { get; set; }

        public CourseController(CourseService service)
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
        public IActionResult Create()
        {
            IEnumerable<CourseViewModel> courses = this.courseService.GetByName();

            bool areCoursesEmpty = courses.Count() == 0;
            if (areCoursesEmpty)
            {
                return this.RedirectToAction("index");
            }

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
    }
}