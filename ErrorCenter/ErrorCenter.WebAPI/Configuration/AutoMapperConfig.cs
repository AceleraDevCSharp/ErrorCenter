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

            CreateMap<ErrorLog, ErrorLogDTO>().ReverseMap();
            CreateMap<ErrorLog, ErrorLogViewModel>()
                .ForMember(x => x.Email, x => x.MapFrom(x => x.User.Email)).ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<SessionResponseDTO, SessionViewModel>().ReverseMap();
        }
    }
}
