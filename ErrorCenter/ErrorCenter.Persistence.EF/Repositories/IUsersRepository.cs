using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Repositories {
  public interface IUsersRepository {
    public Task<User> Create(User user);
    public Task<User> FindByEmail(string email);
  }
}