using AutoMapper;
using ErrorCenter.Services.Models;
using ErrorCenter.WebAPI.ViewModel;
using System.Linq;

namespace ErrorCenter.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ErrorLogViewModel, ErrorLog>();
            CreateMap<ErrorLog, ErrorLogViewModel>()
                .ForMember(x => x.UserEmail, x => x.MapFrom( x => x.User.Email));
        }
    }
}
