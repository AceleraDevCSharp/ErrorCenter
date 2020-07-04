using System.Threading.Tasks;

using ErrorCenter.Services.Models;

namespace ErrorCenter.Services.Interfaces {
  public interface IAuthenticateUserService {
    public Task<Session> Execute(string email, string password);
  }
}