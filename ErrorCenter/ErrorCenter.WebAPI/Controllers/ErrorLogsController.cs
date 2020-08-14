using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Errors;
using Microsoft.AspNetCore.Http;
using ErrorCenter.Services.DTOs;


namespace ErrorCenter.WebAPI.Controllers
{
    [Authorize]
    [Route("v1/error-logs")]
    public class ErrorLogsController : MainController
    {
        private IErrorLogService _errorLogService;
        private readonly IErrorLogRepository<ErrorLog> _errorLogRepository;
        private readonly IMapper _mapper;
        private readonly IDetailsErrorLogService _detailsErrorLogService;

        public ErrorLogsController(IErrorLogService errorLogService,
            IErrorLogRepository<ErrorLog> errorLogRepository,
            IDetailsErrorLogService detailsErrorLogService,
            IMapper mapper)
        {
            _errorLogService = errorLogService;
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
            _detailsErrorLogService = detailsErrorLogService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ErrorLogDTO>> Create([FromBody] ErrorLogDTO newErrorLog)
        {
            newErrorLog.Validate();

            if (newErrorLog.Invalid)
            {
                throw new ViewModelException(
                  "Error while trying to create new error log",
                  StatusCodes.Status400BadRequest,
                  newErrorLog.Notifications
                );
            }

            var identity = User.Identity as ClaimsIdentity;
            List<Claim> claims = identity.Claims.ToList();

            var email = claims.Find(claim => claim.Type == ClaimTypes.Email).Value;

            var errorLog = await _errorLogService.CreateNewErrorLog(newErrorLog, email);

            var createdErrorLog = _mapper.Map<ErrorLogSimpleViewModel>(errorLog);

            return Ok(newErrorLog);

        }

        [HttpPatch("archive/{id:int}")]
        public async Task<ActionResult<ErrorLogViewModel>> Archive(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> claims = identity.Claims.ToList();

            var email = claims
              .Find(claim => claim.Type == ClaimTypes.Email).Value;

            var role = claims
              .Find(claim => claim.Type == ClaimTypes.Role).Value;


            var errorLog = _mapper.Map<ErrorLogViewModel>(await _errorLogService.ArchiveErrorLog(id, email, role));
            return Ok(errorLog);
        }

        [HttpGet("error-details/{id:int}")]
        public async Task<ActionResult<ErrorLogViewModel>> GetErrorLog(int id)
        {
            return _mapper.Map<ErrorLogViewModel>(await _detailsErrorLogService.FindErrorLog(id));
        }

        [HttpPatch("delete/{id:int}")]
        public async Task<ActionResult<ErrorLogViewModel>> Delete(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> claims = identity.Claims.ToList();

            var email = claims.Find(claim => claim.Type == ClaimTypes.Email).Value;

            var role = claims.Find(claim => claim.Type == ClaimTypes.Role).Value;

            var errorLog = _mapper.Map<ErrorLogViewModel>(await _errorLogService.DeleteErrorLog(id, email, role));
            return Ok(errorLog);

        }
    }
}
