using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Services.Interfaces;
using ErrorCenter.Services.Models;
using ErrorCenter.WebAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ErrorCenter.WebAPI.Controllers
{
    [Route("errors")]
    public class ErrorLogListFiltersController : MainController
    {
        private readonly IErrorLogRepository<ErrorLog> _errorLogRepository;
        private readonly IMapper _mapper;

        public ErrorLogListFiltersController(IErrorLogRepository<ErrorLog> errorLogRepository, IMapper mapper)
        {
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetAll()
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectAll());

            return Ok(errors);
        }

        [HttpGet("environment/{environment}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironment(string environment)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironment(environment));

            return Ok(errors);
        }

        [HttpGet("environment/{environment}/{orderby}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironmentOrderBy(string environment, string orderby)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironmentOrderedBy(environment, orderby));

            return Ok(errors);
        }

        [HttpGet("environment/{environment}/{orderby}/{typeSearch}/{textSearch}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironmentOrderBySearchBy(string environment, string orderby, string typeSearch, string textSearch)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironmentOrderedBySearchBy(environment, orderby, typeSearch, textSearch));

            return Ok(errors);
        }
    }
}
