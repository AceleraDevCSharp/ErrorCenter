using System.ComponentModel.DataAnnotations;

namespace ErrorCenter.WebAPI.ViewModel {
  public class LoginInfo {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}