using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.IRepository {
  public interface IUsersRepository {
    public Task<User> FindByEmail(string email);
    public Task<IList<string>> GetRoles(User user);
  }
}