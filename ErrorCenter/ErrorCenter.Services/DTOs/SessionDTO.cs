namespace ErrorCenter.Services.DTOs {
  public class SessionDTO {
    public string Email { get; set; }
    public string Token { get; set; }

    public SessionDTO(string email, string token) {
      Email = email;
      Token = token;
    }
  }
}