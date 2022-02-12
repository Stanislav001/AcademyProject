using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISaveCourseUserService
    {
        Task<bool> SaveStartedCourse(string userId, string courseId);
        Task<bool> RemoveTheCourseAsync(string userId, string courseId);
        bool IsAlreadyStarted(string userId, string courseId);
    }
}
