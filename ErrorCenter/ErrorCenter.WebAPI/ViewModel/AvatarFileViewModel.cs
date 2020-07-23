using Microsoft.AspNetCore.Http;

namespace ErrorCenter.WebAPI.ViewModel {
  public class AvatarFileViewModel {
    public IFormFile avatar { get; set; }
  }
}
