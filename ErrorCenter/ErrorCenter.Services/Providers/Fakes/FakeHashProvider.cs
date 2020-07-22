using Microsoft.AspNetCore.Identity;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Providers.Fakes {
  public class FakeHashProvider : IPasswordHasher<User> {
    public string HashPassword(User user, string password) {
      return password;
    }

    public PasswordVerificationResult VerifyHashedPassword(
      User user,
      string hashedPassword,
      string providedPassword
    ) {
      var verify = hashedPassword.Equals(providedPassword)
        ? PasswordVerificationResult.Success
        : PasswordVerificationResult.Failed;

      return verify;
    }
  }
}
