using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Errors;
using System;

namespace ErrorCenter.WebAPI.Controllers {
  [Route("v1/users/upload-avatar")]
  [ApiController]
  [Authorize]
  public class UserAvatarController : ControllerBase {
    private IUserAvatarUploadService userAvatarUploadService;

    public UserAvatarController(
      IUserAvatarUploadService userAvatarUploadService
    ) {
      this.userAvatarUploadService = userAvatarUploadService;
    }

    [HttpPatch]
    public async Task<ActionResult<dynamic>> Update(
      [FromForm] UserAvatarDTO file
    ) {
      file.Validate();

      if (file.Invalid) {
        throw new FileUploadException(
          "No file was uploaded",
          400
        );
      }

      var user_email = HttpContext
        .User
        .Claims
        .Where(
          x => x.Type == ClaimTypes.Email
        )
        .FirstOrDefault()
        .Value;

      var avatarUrl = await userAvatarUploadService
        .UploadUserAvatar(user_email, file);

      return JsonConvert.SerializeObject(avatarUrl);
    }
  }
}
