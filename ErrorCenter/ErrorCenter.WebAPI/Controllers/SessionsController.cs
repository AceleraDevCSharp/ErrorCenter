using System.Threading.Tasks;

using AutoMapper;
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
    private IMapper _mapper;

    public SessionsController(
      IAuthenticateUserService service,
      IMailToUserService mail,
      IMapper mapper
    ) {
        _service = service;
        _mail = mail;
        _mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<SessionViewModel>> Create(
      [FromBody] SessionRequestDTO login
    ) {
      login.Validate();

      if (login.Invalid) {
        throw new ViewModelException(
          "Error while trying to authenticate",
          StatusCodes.Status401Unauthorized,
          login.Notifications
        );
      }

      var sessionData = await _service.Authenticate(login);

      var session = _mapper.Map<SessionViewModel>(sessionData);

      return Ok(session);
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
