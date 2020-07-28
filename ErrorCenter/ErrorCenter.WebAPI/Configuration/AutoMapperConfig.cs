using AutoMapper;
using System.Linq;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ErrorLogViewModel, ErrorLog>();
            CreateMap<ErrorLog, ErrorLogViewModel>()
                .ForMember(x => x.UserEmail, x => x.MapFrom( x => x.User.Email));
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
