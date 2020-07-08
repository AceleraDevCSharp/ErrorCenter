using AutoMapper;
using ErrorCenter.Services.Models;
using ErrorCenter.WebAPI.ViewModel;

namespace ErrorCenter.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ErrorLog, ErrorLogViewModel>().ReverseMap();
        }
    }
}
