using AutoMapper;

using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
        }
    }
}