using Flunt.Validations;
using Flunt.Notifications;
using Microsoft.AspNetCore.Http;

namespace ErrorCenter.Services.DTOs {
  public class UserAvatarDTO : Notifiable, IValidatable {
    public IFormFile avatar { get; set; }
    
    public void Validate() {
      AddNotifications(new Contract()
        .IsNotNull(avatar, "avatar", "No file was uploaded")
      );
    }
  }
}
