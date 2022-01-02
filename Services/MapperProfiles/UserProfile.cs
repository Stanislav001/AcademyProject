using AutoMapper;

using Models.Models;
using Services.ViewModels;

namespace Services.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}