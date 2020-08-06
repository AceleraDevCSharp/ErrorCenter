using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IErrorLogService errorLogService;
        private readonly IMapper mapper;

        public ErrorLogsController(IErrorLogService service, IMapper mapper)
        {
            errorLogService = service;
            this.mapper = mapper;
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

            var errorLog = await errorLogService.CreateNewErrorLog(newErrorLog, email);

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

            var errorLog = await errorLogService.DeleteErrorLog(id, email, role);
            return errorLog;
        }
    }
}
