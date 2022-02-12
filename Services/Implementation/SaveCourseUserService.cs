using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Date;
using Models.Models;
using Services.Interfaces;
using Models.Constants.ExeptionConstants;

namespace Services.Implementation
{
    public class SaveCourseUserService : ISaveCourseUserService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;
        public SaveCourseUserService(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<bool> SaveStartedCourse(string userId, string courseId)
        {
            await CheckIfUserAndCourseExistAsync(userId, courseId);

            if (this.IsAlreadyStarted(userId, courseId))
            {
                return false;
            }

            SaveCourseUser courseUser = new SaveCourseUser()
            {
                UserId = userId,
                CourseId = courseId
            };

            await SetCourse(courseId);

            await this.dbContext.SaveCourseUsers.AddAsync(courseUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveTheCourseAsync(string userId, string courseId)
        {
            await CheckIfUserAndCourseExistAsync(userId, courseId);

            if (this.IsAlreadyStarted(userId, courseId) == false)
            {
                return false;
            }

            SaveCourseUser courseUser = GetStarted(userId, courseId);
            await RemoveCourse(courseId);

            this.dbContext.SaveCourseUsers.Remove(courseUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool IsAlreadyStarted(string userId, string courseId)
        {
            SaveCourseUser courseUser = GetStarted(userId, courseId);

            bool isAlreadyStarted = courseUser != null;

            return isAlreadyStarted;
        }

        public async Task SetCourse(string courseId)
        {
            Course course = this.dbContext.Courses
                .Where(m => m.Id == courseId)
                .SingleOrDefault();

            course.isStarted = true;

            this.dbContext.Update(course);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task RemoveCourse(string courseId)
        {
            Course course = this.dbContext.Courses
                .Where(x => x.Id == courseId)
                .SingleOrDefault();
               
            course.isStarted = false;
            
            this.dbContext.Update(course);
            await this.dbContext.SaveChangesAsync();
        }

        private SaveCourseUser GetStarted(string userId, string courseId)
        {
            SaveCourseUser courseUser = this.dbContext.SaveCourseUsers
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