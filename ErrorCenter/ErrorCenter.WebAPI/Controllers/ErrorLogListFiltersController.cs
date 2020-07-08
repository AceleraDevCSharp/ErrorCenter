using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Services.Interfaces;
using ErrorCenter.WebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ErrorCenter.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ErrorLogListFiltersController : MainController
    {
        private readonly IErrorLogRepository<ErrorLogViewModel> _errorLogRepository;
        private readonly IMapper _mapper;

        public ErrorLogListFiltersController(IErrorLogRepository<ErrorLogViewModel> errorLogRepository, IMapper mapper)
        {
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ErrorLogViewModel>> GetAll()
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>( _errorLogRepository.SelectAllWithoutDuplicated());

            return errors;
        }
    }
}
