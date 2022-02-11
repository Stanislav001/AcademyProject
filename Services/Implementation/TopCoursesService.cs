using System.Collections.Generic;
using Services.ViewModels.RankingViewModels;

using Date;
using System.Linq;
using Services.Interfaces;

namespace Services.Implementation
{
    public class TopCoursesService : ITopCoursesService
    {
        private readonly ApplicationDbContext dbContext;

        public TopCoursesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TopCourseViewModel> GetAll()
        {
            IEnumerable<TopCourseViewModel> courses = this.dbContext.Courses
                .Select(course => new TopCourseViewModel
                {
                    Id = course.Id,
                    CourseName = course.CourseName,
                    Description = course.Description,
                    Duration = course.Duration,
                    Price = course.Price,
                    Votes = course.Votes,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    ImageName = course.ImageName,
                    ImageFile = course.ImageFile
                }).OrderByDescending(x => x.Votes).Take(10).ToList();

            return courses;
        }
    }
}