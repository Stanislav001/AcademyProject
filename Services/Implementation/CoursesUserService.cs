using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

using Date;
using System;
using Models.Models;
using Services.Interfaces;
using Models.Constants.ExeptionConstants;

namespace Services.Implementation
{
    public class CoursesUserService : ICoursesUserService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;
        public CoursesUserService(ApplicationDbContext dbContext, UserManager<User> userManager) 
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
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
                CourseId = courseId,
            };

            await AddVote(courseId);

            await this.dbContext.CourseUsers.AddAsync(courseUser);
            await this.dbContext.SaveChangesAsync();

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

            this.dbContext.CourseUsers.Remove(courseUser);
            await this.dbContext.SaveChangesAsync();

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
            Course course = this.dbContext.Courses
                .Where(m => m.Id == courseId)
                .SingleOrDefault();

            course.Votes += 1;

            this.dbContext.Update(course);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveVote(string courseId)
        {
            Course course = this.dbContext.Courses
                .Where(x => x.Id == courseId)
                .SingleOrDefault();

            if (course.Votes <= 0)
            {
                throw new Exception("Invalid operation");
            }

            course.Votes -= 1;

            this.dbContext.Update(course);
            await this.dbContext.SaveChangesAsync();
        }

        private CourseUser GetVoted(string userId, string courseId)
        {
            CourseUser courseUser = this.dbContext.CourseUsers
                .Where(x => x.UserId == userId && x.CourseId == courseId)
                .FirstOrDefault();

            return courseUser;
        }

        private async Task CheckIfUserAndCourseExistAsync(string userId, string courseId)
        {
            Course course = this.dbContext.Courses
                .Where(x => x.Id == courseId)
                .SingleOrDefault();

            bool isCourseNotExists = course == null;

            if (isCourseNotExists)
            {
                throw new ArgumentException(ExeptionConstants.NOT_EXISTING_COURSE_ERROR_MESSAGE);
            }

            if (await this.userManager.FindByIdAsync(userId) == null)
            {
                throw new ArgumentException(ExeptionConstants.NOT_EXISTING_USER_ERROR_MESSAGE);
            }
        }
    }
}