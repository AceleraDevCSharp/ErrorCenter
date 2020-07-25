using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.WebAPI.Controllers {
  [Route("v1/sessions")]
  public class SessionsController : MainController {
    private IAuthenticateUserService _service;
    private IMailToUserService _mail;

    public SessionsController(
      IAuthenticateUserService service,
      IMailToUserService mail
    ) {
        _service = service;
        _mail = mail;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SessionDTO>> Create(
      [FromBody] LoginInfoViewModel login
    ) {
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

    [HttpPost("mail")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> ForgottenPassword(
      string user_mail
    ) {
        return await _mail.MailToUser(user_mail);
    }
  }
}
