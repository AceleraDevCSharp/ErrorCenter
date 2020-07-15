namespace ErrorCenter.Persistence.EF.Models {
  public class Session {
    public string Email { get; set; }
    public string Environment { get; set; }
    public string Token { get; set; }

    public Session(string email, string environment, string token) {
      Email = email;
      Environment = environment;
      Token = token;
    }
  }
}