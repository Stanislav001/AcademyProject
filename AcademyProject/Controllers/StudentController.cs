using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.ViewModels;
using System.Collections.Generic;

namespace AcademyProject.Controllers
{
    public class StudentController : Controller
    {
        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        public IStudentService studentService { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<StudentViewModel> students = this.studentService.GetAll();

            return this.View(students);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            StudentViewModel student = studentService.GetDetailsById(id);

            bool isStudentNull = student == null;
            if (isStudentNull)
            {
                return this.RedirectToAction("index");
            }
            return this.View(student);
        }
    }
}
