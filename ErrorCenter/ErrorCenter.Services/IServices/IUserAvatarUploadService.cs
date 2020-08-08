using System;
using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Services.IServices {
  public interface IUserAvatarUploadService {
    public Task<string> UploadUserAvatar(
      string user_email,
      UserAvatarDTO avatar
    );
  }
}
