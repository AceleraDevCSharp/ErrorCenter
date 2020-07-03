using System.ComponentModel.DataAnnotations;

namespace ErrorCenter.WebAPI.Models {
  public class LoginInfo {
    public string Email { get; set; }
    public string Password { get; set; }
  }
}