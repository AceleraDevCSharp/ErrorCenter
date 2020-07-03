using System.Threading.Tasks;
using System.Collections.Generic;
using ErrorCenter.Domain;

namespace ErrorCenter.Persistence.EF.Repositories.Fakes {
  public class FakeUsersRepository : IUsersRepository {
    private List<User> users = new List<User>();

    public async Task<User> Create(User user) {
      users.Add(user);
      await Task.Delay(10);
      return user;
    }

    public async Task<User> FindByEmail(string email) {
      var user = users.Find(x => x.Email == email);
      await Task.Delay(10);
      return user;
    }
  }
}