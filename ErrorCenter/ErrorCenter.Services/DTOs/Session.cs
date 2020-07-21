namespace ErrorCenter.Services.DTOs {
  public class Session {
    public string Email { get; set; }
    public string Token { get; set; }

    public Session(string email, string token) {
      Email = email;
      Token = token;
    }
  }
}