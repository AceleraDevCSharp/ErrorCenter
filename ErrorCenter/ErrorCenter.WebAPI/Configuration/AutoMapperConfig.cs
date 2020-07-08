
using AutoMapper;
using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Repository.Model;
using ErrorCenter.WebAPI.ViewModel;

namespace ErrorCenter.WebAPI.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ErrorLogModel, ErrorLogViewModel>().ReverseMap();
        }
    }
}
