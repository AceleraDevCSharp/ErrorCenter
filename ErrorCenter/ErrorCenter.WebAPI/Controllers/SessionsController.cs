using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.WebAPI.Models;
using ErrorCenter.Services;
using ErrorCenter.Services.Models;

namespace ErrorCenter.WebAPI.Controllers {
  [ApiController]
  [Route("v1/sessions")]
  public class SessionsController : ControllerBase {
    private AuthenticateUserService _service;

    public SessionsController(AuthenticateUserService service) {
      _service = service;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<Session>> Create(
      [FromBody] LoginInfo login,
      [FromServices] AuthenticateUserService service
    ) {
      var session = service.execute(login.Email, login.Password);

      if (session == null) return NotFound(new { 
        message = "Combinação e-mail/senha inválida"
      });

      return session;
    }
  }
}