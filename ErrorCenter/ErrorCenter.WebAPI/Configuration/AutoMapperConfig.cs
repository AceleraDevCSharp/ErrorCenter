using AutoMapper;
using System.Linq;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Models;

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
