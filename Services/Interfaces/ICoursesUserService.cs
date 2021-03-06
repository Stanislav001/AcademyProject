using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICoursesUserService
    {
        Task<bool> EnrollUserToVoteAsync(string userId, string courseId);
        Task<bool> RemoveTheUserVoteAsync(string userId, string courseId);
        bool IsAlreadyVoted(string userId, string courseId);
    }
}