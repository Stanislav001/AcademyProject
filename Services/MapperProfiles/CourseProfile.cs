using AutoMapper;

using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<Course, CourseViewModel>().ReverseMap();
        }
    }
}