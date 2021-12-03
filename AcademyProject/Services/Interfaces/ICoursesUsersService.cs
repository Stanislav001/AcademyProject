using System.Threading.Tasks;

namespace AcademyProject.Services.Interfaces
{
    public interface ICoursesUsersService
    {
        Task<bool> EnrollUserToVoteAsync(string userId, string courseId);

        Task<bool> RemoveTheUserVoteAsync(string userId, string courseId);

        bool IsAlreadyVoted(string userId, string courseId);
    }
}
