using System;
using ErrorCenter.Domain;

namespace ErrorCenter.Persistence.EF.Repositories {
  public interface IUsersRepository {
    public User Create(User user);
    public User FindByEmail(string email);
  }
}