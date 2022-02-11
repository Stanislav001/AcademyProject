using System.Threading.Tasks;

using Models.Models;

namespace Services.Interfaces
{
    public interface ICoursesUserService
    {
        public Task<bool> EnrollUserToVoteAsync(string userId, string courseId);
        public Task<bool> RemoveTheUserVoteAsync(string userId, string courseId);
        public bool IsAlreadyVoted(string userId, string courseId);
        public Task AddVote(string courseId);
        public Task RemoveVote(string courseId);
        public CourseUser GetVoted(string userId, string courseId);
        public Task CheckIfUserAndCourseExistAsync(string userId, string courseId);
    }
}