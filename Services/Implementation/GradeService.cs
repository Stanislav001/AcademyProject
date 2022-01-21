using AutoMapper;
using Date;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly IStudentService studentService;

        public GradeService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> CreateGradeAsync(string courseId, string studentId)
        {
            Grade garde = new Grade
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await this.DbContext.Grades.AddAsync(garde);

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletGradeAsync(string gradeId)
        {
            var grade = await this.DbContext.Grades
                .FirstOrDefaultAsync(g => g.Id == gradeId);

            if (grade == null)
            {
                return false;
            }

            this.DbContext.Grades.Remove(grade);

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GradeViewModel>> GetAllGradesAsync(string id)
        {
            var grades = await this.DbContext.Grades
                .Where(g => g.Id == id )
                .Select(grade => new GradeViewModel
                {
                    CourseId  = grade.CourseId,
                    StudentGrade = grade.StudentGrade,
                    StudentId = grade.StudentId,
                    Id = grade.Id
                })
                .ToArrayAsync();

            return grades;
        }

        public Grade GetGradeById(string gradeId)
        {
            var item = this.DbContext.Grades.FirstOrDefault(g => g.Id == gradeId);

            return item;
        }

        // TODO: Refactoring
        public async Task<bool> CreateGradeAsync(string courseId, string studentId, int gradeContext)
        {
            Grade grade = new Grade
            {
                Id = new Guid().ToString(),
                CourseId = courseId,
                StudentId = studentId,
                StudentGrade = gradeContext
            };

            await this.DbContext.Grades.AddAsync(grade);

            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}