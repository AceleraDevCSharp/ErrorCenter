namespace ErrorCenter.Services.Providers.HashProvider.Models {
  public interface IHashProvider {
    public string GenerateHash(string input);
    public bool VerifyHash(string input, string hash);
  }
}