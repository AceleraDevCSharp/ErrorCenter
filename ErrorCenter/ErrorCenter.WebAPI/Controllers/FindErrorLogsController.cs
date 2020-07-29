using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.WebAPI.Controllers
{
    [Authorize]
    [Route("v1/error-logs")]
    public class FindErrorLogsController : MainController
    {
        private readonly IGetErrorLogService<ErrorLog> _errorLogRepository;
        private readonly IMapper _mapper;

        public FindErrorLogsController(IGetErrorLogService<ErrorLog> errorLogRepository, IMapper mapper)
        {
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetAll()
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectAll());

            return Ok(errors);
        }

        [AllowAnonymous]
        [HttpGet("environments")]
        public async Task<ActionResult<string>> GetEnvironments()
        {
            return Ok(await _errorLogRepository.Environments());
        }

        [HttpGet("archived")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetLogsArchived()
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectArchived());

            return Ok(errors);
        }

        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetLogsDeleted()
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectDeleted());

            return Ok(errors);
        }

        [HttpGet("environment={environment}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironment(string environment)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironment(environment));

            return Ok(errors);
        }

        [HttpGet("environment={environment}/orderby={orderby}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironmentOrderBy(string environment, string orderby)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironmentOrderedBy(environment, orderby));

            return Ok(errors);
        }

        [HttpGet("environment={environment}/orderby={orderby}/typeSearch={typeSearch}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironmentOrderBySearchBy(string environment, string orderby, string typeSearch, string textSearch)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironmentOrderedBySearchBy(environment, orderby, typeSearch, textSearch));

            return Ok(errors);
        }

        [HttpGet("environment={environment}/typeSearch={typeSearch}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetByEnvironmentSearchBy(string environment, string typeSearch, string textSearch)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectByEnvironmentSearchBy(environment, typeSearch, textSearch));

            return Ok(errors);
        }

        [HttpGet("orderby={orderby}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetOrderedBy(string orderby)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectOrderedBy(orderby));

            return Ok(errors);
        }

        [HttpGet("orderby={orderby}/typeSearch={typeSearch}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetOrderedBySearchBy(string orderby, string typeSearch, string textSearch)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectOrderedBySearchBy(orderby, typeSearch, textSearch));

            return Ok(errors);
        }

        [HttpGet("typeSearch={typeSearch}")]
        public async Task<ActionResult<IEnumerable<ErrorLogViewModel>>> GetSearchBy(string typeSearch, string textSearch)
        {
            var errors = _mapper.Map<IEnumerable<ErrorLogViewModel>>(await _errorLogRepository.SelectSearchBy(typeSearch, textSearch));

            return Ok(errors);
        }

    }
}
