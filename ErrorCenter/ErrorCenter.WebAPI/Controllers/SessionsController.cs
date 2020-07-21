using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Errors;
using ErrorCenter.Persistence.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ErrorCenter.WebAPI.Controllers {
  [Route("v1/sessions")]
  public class SessionsController : MainController {
    private IAuthenticateUserService _service;
    private UserManager<User> _manager;

    public SessionsController(IAuthenticateUserService service, UserManager<User> manager) {
      _service = service;
      _manager = manager;
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

      var session = await _service.Execute(login.Email, login.Password);

      return session;
    }
  }
}