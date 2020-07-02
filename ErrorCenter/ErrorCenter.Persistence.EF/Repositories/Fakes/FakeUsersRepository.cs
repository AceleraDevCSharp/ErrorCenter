using System;
using System.Collections.Generic;
using ErrorCenter.Domain;

namespace ErrorCenter.Persistence.EF.Repositories.Fakes {
  public class FakeUsersRepository : IUsersRepository {
    private List<User> users = new List<User>();
    private bool disposed = false;

    public User Create(User user) {
      users.Add(user);
      return user;
    }

    public User FindById(int id) {
      var user = users.Find(x => x.Id == id);
      return user;
    }
  }
}