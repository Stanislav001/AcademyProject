﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Date;
using Models.Models;
using Services.Interfaces;
using Services.ViewModels;

namespace Services.Implementation
{
    public class GradeService : BaseService, IGradeService
    {
        private readonly IStudentService studentService;

        public GradeService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public async Task<bool> CreateGradeAsync(string courseId, string studentId, int studentGrade)
        {
            Grade garde = new Grade
            {
                Id = Guid.NewGuid().ToString(),
                CourseId = courseId,
                StudentId = studentId,
                StudentGrade = studentGrade
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
        public async Task<bool> CreateGradeAsync(string courseId, string studentId)
        {
            Grade grade = new Grade
            {
                Id = new Guid().ToString(),
                CourseId = courseId,
                StudentId = studentId,
            };

            await this.DbContext.Grades.AddAsync(grade);

            await this.DbContext.SaveChangesAsync();

            return true;
        }
    }
}