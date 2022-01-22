using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using Date;
using Microsoft.EntityFrameworkCore;

namespace AcademyProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(IStudentService studentService, IGradeService gradeService)
        {
            this.studentService = studentService;
            this.gradeService = gradeService;
        }
        public IStudentService studentService { get; set; }
        public IGradeService gradeService { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<StudentViewModel> students = this.studentService.GetAll();

            return this.View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<StudentViewModel> student = this.studentService.GetByName();

            bool areStudentEmpty = student.Count() == 0;
            if (areStudentEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.students = student;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Student studentDb = this.studentService.GetByModelName(model.FirstName);

            bool isStudentAlreadyInDb = studentDb != null;
            if (isStudentAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }
            await this.studentService.CreateAsync(model);

            return this.RedirectToAction("index");
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

        [HttpGet]
        public IActionResult Update(string id)
        {
            StudentViewModel student = this.studentService.UpdateById(id);

            return this.View(student);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(StudentViewModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.studentService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            await this.studentService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}