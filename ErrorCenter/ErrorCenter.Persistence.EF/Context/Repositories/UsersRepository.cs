using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using ErrorCenter.Persistence.EF.Repositories;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Context.Repositories {
  public class UsersRepository : IUsersRepository {
    private ErrorCenterDbContext Context;

    public UsersRepository(ErrorCenterDbContext context) {
      Context = context;
    }

    public async Task<User> FindByEmail(string email) {
      var user = await Context
        .Users
        .Where(x => x.Email == email)
        .AsNoTracking()
        .FirstOrDefaultAsync();
      return user;
    }

    public async Task<User> Create(User user) {
      Context.Users.Add(user);
      await Context.SaveChangesAsync();
      return user;
    }
  }
}