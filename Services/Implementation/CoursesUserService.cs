using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

using Date;
using Models.Models;
using Services.Interfaces;

namespace Services.Implementation
{
    public class CoursesUserService : BaseService , ICoursesUserService
    {
        private readonly UserManager<User> userManager;

        public CoursesUserService(ApplicationDbContext dbContext, UserManager<User> userManager, IMapper mapper) : base(dbContext, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<bool> EnrollUserToVoteAsync(string userId, string courseId)
        {
            await CheckIfUserAndCourseExistAsync(userId, courseId);

            if (this.IsAlreadyVoted(userId, courseId))
            {
                return false;
            }

            CourseUser courseUser = new CourseUser()
            {
                UserId = userId,
                CourseId = courseId
            };

            await AddVote(courseId);

            await this.DbContext.CourseUsers.AddAsync(courseUser);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveTheUserVoteAsync(string userId, string courseId)
        {
            await CheckIfUserAndCourseExistAsync(userId, courseId);

            if (this.IsAlreadyVoted(userId, courseId) == false)
            {
                return false;
            }

            CourseUser courseUser = GetVoted(userId, courseId);

            await RemoveVote(courseId);

            this.DbContext.CourseUsers.Remove(courseUser);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public bool IsAlreadyVoted(string userId, string courseId)
        {
            CourseUser courseUser = GetVoted(userId, courseId);

            bool isAlreadyVoted = courseUser != null;

            return isAlreadyVoted;
        }

        public async Task AddVote(string courseId)
        {
            Course course = this.DbContext.Courses
                            .Where(x => x.Id == courseId)
                            .FirstOrDefault();

            course.Votes += 1;

            this.DbContext.Update(course);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task RemoveVote(string courseId)
        {
            Course course = this.DbContext.Courses
                            .Where(x => x.Id == courseId)
                            .FirstOrDefault();

            if (course.Votes <= 0)
            {
                System.Console.WriteLine("Invalid operation");
            }

            course.Votes -= 1;

            this.DbContext.Update(course);
            await this.DbContext.SaveChangesAsync();
        }

        public CourseUser GetVoted(string userId, string courseId)
        {
            CourseUser courseUser = this.DbContext.CourseUsers
                                    .Where(x => x.UserId == userId && x.CourseId == courseId)
                                    .FirstOrDefault();

            return courseUser;
        }

        public async Task CheckIfUserAndCourseExistAsync(string userId, string courseId)
        {
            Course course = this.DbContext.Courses
                            .Where(c => c.Id == courseId)
                            .SingleOrDefault();

            bool IsCourseNotExist = course == null;

            if (IsCourseNotExist)
            {
                System.Console.WriteLine("This course is not exist");
            }

            if (await this.userManager.FindByIdAsync(userId) == null)
            {
                System.Console.WriteLine("This user is not exist");
            }
        }
    }
}