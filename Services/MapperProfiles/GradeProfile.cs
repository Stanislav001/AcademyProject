using AutoMapper;
using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class GradeProfile : Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeViewModel>().ReverseMap();
        }
    }
}
