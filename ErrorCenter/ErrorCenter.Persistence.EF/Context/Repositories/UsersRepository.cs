using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.Persistence.EF.Context.Repositories {
  public class UsersRepository : IUsersRepository {
    private ErrorCenterDbContext Context;

    public UsersRepository(ErrorCenterDbContext context) {
      Context = context;
    }

    public User FindById(int id) {
      var user = Context.Users.Find(id);
      return user;
    }

    public User Create(User user) {
      Context.Users.Add(user);
      Context.SaveChanges();
      return user;
    }
  }
}