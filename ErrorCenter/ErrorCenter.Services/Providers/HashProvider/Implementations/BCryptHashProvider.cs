using ErrorCenter.Services.Providers.HashProvider.Models;

namespace ErrorCenter.Services.Providers.HashProvider.Implementations {
  public class BCryptHashProvider : IHashProvider {
    public string GenerateHash(string input) {
      string hashedPassword = BCrypt.Net.BCrypt.HashPassword(input);
      return hashedPassword;
    }

    public bool VerifyHash(string input, string hash) {
      return BCrypt.Net.BCrypt.Verify(input, hash);
    }
  }
}