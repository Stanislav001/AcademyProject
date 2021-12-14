using Models.Models;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IGradeService
    {
        public Task<bool> CreateGradeAsync(string courseId, string studentId, string senderId);

        public Task<bool> DeletGradeAsync(string gradeId);

        public Task<IEnumerable<GradeViewModel>> GetAllGradesAsync(string id);

        public Grade GetGradeById(string gradeId);

    }
}
