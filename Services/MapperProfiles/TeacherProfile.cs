using AutoMapper;

using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<Teacher, TeacherViewModel>().ReverseMap();
        }
    }
}