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

        public ErrorLogsController(IErrorLogService archiveSerivce, 
            IErrorLogRepository<ErrorLog> errorLogRepository,
            IMapper mapper)
        {
            _archiveService = archiveSerivce;
            _errorLogRepository = errorLogRepository;
            _mapper = mapper;
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
            return errorLog;
        }

        [HttpGet("error-details/{id:int}")]
        public async Task<ActionResult<ErrorLogViewModel>> GetErrorLog(int id)
        {
            return _mapper.Map<ErrorLogViewModel>(await _errorLogRepository.FindById(id));
        }
    }
}
