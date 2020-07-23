using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using System.Linq;
using System.Security.Claims;

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
      [FromForm] AvatarFileViewModel file
    ) {
      var user_email = HttpContext
        .User
        .Claims
        .Where(
          x => x.Type == ClaimTypes.Email
        )
        .FirstOrDefault()
        .Value;

      var avatarUrl = await userAvatarUploadService
        .UploadUserAvatar(user_email, new UserAvatarDTO(file.avatar));

      return new { avatarUrl };
    }
  }
}
