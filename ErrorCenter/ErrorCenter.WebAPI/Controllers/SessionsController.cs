using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ErrorCenter.WebAPI.Controllers {
  [Route("v1/sessions")]
  public class SessionsController : MainController {
    private IAuthenticateUserService _service;

    public SessionsController(IAuthenticateUserService service) {
      _service = service;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Session>> Create([FromBody] LoginInfo login) {
      login.Validate();

      if (login.Invalid) {
        throw new ViewModelException(
          "Error while trying to authenticate",
          StatusCodes.Status401Unauthorized,
          login.Notifications
        );
      }

      var session = await _service.Authenticate(login.Email, login.Password);

      return session;
    }
  }
}