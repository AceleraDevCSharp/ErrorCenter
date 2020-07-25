using Microsoft.AspNetCore.Http;

namespace ErrorCenter.Services.DTOs {
  public class UserAvatarDTO {
    public IFormFile avatar { get; set; }

    public UserAvatarDTO(IFormFile avatar) {
      this.avatar = avatar;
    }
  }
}
