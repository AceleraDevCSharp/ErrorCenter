using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ErrorCenter.Services.Errors;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.WebAPI.Controllers {
  [Authorize]
  [Route("v1/error-logs")]
  public class ErrorLogsController : MainController {
    private IArchiveErrorLogService _archiveService;

    public ErrorLogsController(IArchiveErrorLogService archiveSerivce) {
      _archiveService = archiveSerivce;
    }

    [HttpPatch("archive/{id:int}")]
    public async Task<ActionResult<ErrorLog>> Archive(int id) {
      var identity = User.Identity as ClaimsIdentity;
      IEnumerable<Claim> claims = identity.Claims;

      var email = claims
        .Where(x => x.Type == ClaimTypes.Email)
        .FirstOrDefault().Value;

      var errorLog = await _archiveService.Execute(id, email);
      return errorLog;
    }
  }
}
