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
    public class GradeService : IGradeService
    {
        public ApplicationDbContext dbContext { get; set; }
        public GradeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateGradeAsync(string courseId, string studentId, string senderId)
        {
            Grade garde = new Grade
            {
                CourseId = courseId,
                StudentId = studentId
            };

            await this.dbContext.Grades.AddAsync(garde);

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletGradeAsync(string gradeId)
        {
            var grade = await this.dbContext.Grades
                .FirstOrDefaultAsync(g => g.Id == gradeId);

            if (grade == null)
            {
                return false;
            }

            this.dbContext.Grades.Remove(grade);

            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GradeViewModel>> GetAllGradesAsync(string id)
        {
            var grades = await this.dbContext.Grades
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
            var item = this.dbContext.Grades.FirstOrDefault(g => g.Id == gradeId);

            return item;
        }
    }
}
