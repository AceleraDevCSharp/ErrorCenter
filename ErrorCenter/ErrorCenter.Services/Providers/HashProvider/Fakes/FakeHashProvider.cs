using ErrorCenter.Services.Providers.HashProvider.Models;

namespace ErrorCenter.Services.Providers.HashProvider.Fakes {
  public class FakeHashProvider : IHashProvider {
    public string GenerateHash(string input) {
      return input;
    }

    public bool VerifyHash(string input, string hash) {
      return input.Equals(hash);
    }
  }
}