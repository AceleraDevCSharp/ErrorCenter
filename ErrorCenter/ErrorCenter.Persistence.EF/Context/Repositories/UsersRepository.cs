using System.Linq;

using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.Persistence.EF.Context.Repositories {
  public class UsersRepository : IUsersRepository {
    private ErrorCenterDbContext Context;

    public UsersRepository(ErrorCenterDbContext context) {
      Context = context;
    }

    public User FindByEmail(string email) {
      var user = Context.Users.Where(x => x.Email == email).FirstOrDefault();
      return user;
    }

    public User Create(User user) {
      Context.Users.Add(user);
      Context.SaveChanges();
      return user;
    }
  }
}