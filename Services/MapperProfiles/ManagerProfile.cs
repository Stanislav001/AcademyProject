using AutoMapper;

using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<Manager, ManagerViewModel>().ReverseMap();
        }
    }
}