using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.Interfaces;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.WebAPI.Controllers {
  [ApiController]
  [Route("v1/sessions")]
  public class SessionsController : ControllerBase {
    private IAuthenticateUserService _service;

    public SessionsController(IAuthenticateUserService service) {
      _service = service;
    }

    [HttpPost]
    [Route("")]
    [AllowAnonymous]
    public async Task<ActionResult<Session>> Create([FromBody] LoginInfo login) {
      var session = await _service.Execute(login.Email, login.Password);

      if (session == null) return BadRequest(new { 
        message = "Combinação e-mail/senha inválida"
      });

      return session;
    }
  }
}