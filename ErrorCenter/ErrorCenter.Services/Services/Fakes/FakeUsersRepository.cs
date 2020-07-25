using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Services.Fakes {
  public class FakeUsersRepository : IUsersRepository {
    private List<User> users = new List<User>();

    public async Task<User> Create(User user) {
      users.Add(user);
      await Task.Delay(10);
      return user;
    }

    public async Task<User> Save(User user) {
      throw new NotImplementedException();
    }

    public async Task<User> FindByEmail(string email) {
      var user = users.Find(x => x.Email == email);
      await Task.Delay(1);
      return user;
    }

    public async Task<User> FindByEmailTracking(string email) {
      return await FindByEmail(email);
    }

    public async Task<IList<string>> GetUserRoles(User user) {
      var roles = new List<string>() { "Development", "Homologation", "Production" };

      var rnd = new Random();
      var userRoles = new List<string>() { roles[rnd.Next(3)] };

      await Task.Delay(1);

      return userRoles;
    }
  }
}