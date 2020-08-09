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

using System;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.Errors;
using Microsoft.AspNetCore.Http;
using ErrorCenter.Services.DTOs;
using AutoMapper;


namespace ErrorCenter.WebAPI.Controllers
{
    [Authorize]
    [Route("v1/error-logs")]
    public class ErrorLogsController : MainController
    {

        private IErrorLogService _archiveService;
        private readonly IErrorLogRepository<ErrorLog> _errorLogRepository;
        private readonly IMapper _mapper;

        public ErrorLogsController(IErrorLogService archiveService, IErrorLogRepository<ErrorLog> errorLogRepository,
                                   IMapper mapper) 
        {
            _archiveService = archiveService;
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ErrorLogSimpleViewModel>> Create([FromBody] ErrorLogDTO newErrorLog)
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

            var errorLog = await _archiveService.CreateNewErrorLog(newErrorLog, email);

            var createdErrorLog = mapper.Map<ErrorLogSimpleViewModel>(errorLog);

            return createdErrorLog;

        }

        [HttpPatch("archive/{id:int}")]
        public async Task<ActionResult<ErrorLog>> Archive(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> claims = identity.Claims.ToList();

            var email = claims
              .Find(claim => claim.Type == ClaimTypes.Email).Value;

            var role = claims
              .Find(claim => claim.Type == ClaimTypes.Role).Value;


            var errorLog = await _archiveService.ArchiveErrorLog(id, email, role);
            return Ok(errorLog);
        }

        [HttpGet("error-details/{id:int}")]
        public async Task<ActionResult<ErrorLogViewModel>> GetErrorLog(int id)
        {
            return _mapper.Map<ErrorLogViewModel>(await _errorLogRepository.FindById(id));

            var errorLog = await errorLogService.ArchiveErrorLog(id, email, role);
            return errorLog;
        }

        [HttpPatch("delete/{id:int}")]
        public async Task<ActionResult<ErrorLog>> Delete(int id)
        {
            var identity = User.Identity as ClaimsIdentity;
            List<Claim> claims = identity.Claims.ToList();

            var email = claims.Find(claim => claim.Type == ClaimTypes.Email).Value;

            var role = claims.Find(claim => claim.Type == ClaimTypes.Role).Value;

            var errorLog = await _archiveService.DeleteErrorLog(id, email, role);
            return errorLog;

        }
    }
}
