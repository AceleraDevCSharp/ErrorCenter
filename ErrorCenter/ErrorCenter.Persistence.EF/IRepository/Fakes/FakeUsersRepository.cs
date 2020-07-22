using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.IRepository;
using Microsoft.AspNetCore.Identity;

namespace ErrorCenter.Persistence.EF.Repositories.Fakes {
  public class FakeUsersRepository : IUsersRepository {
    private List<User> users = new List<User>();
    private List<Environment> environments = new List<Environment>();
    private List<IdentityUserRole<string>> userRoles =
      new List<IdentityUserRole<string>>();

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

    public async Task<IList<string>> GetRoles(User user) {
      var roleId = userRoles
        .Where(x => x.UserId == user.Id)
        .FirstOrDefault();

      var roles = environments
        .Where(x => x.Id.Equals(roleId));

      IList<string> rolesNames = new List<string>();
      foreach (var role in roles)
        rolesNames.Add(role.Name);

      await Task.Delay(1);

      return rolesNames;
    }
  }
}