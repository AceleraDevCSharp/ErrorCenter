using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.IRepository;

namespace ErrorCenter.Persistence.EF.Repository {
  public class UsersRepository : IUsersRepository {
    private ErrorCenterDbContext Context;
    private UserManager<User> Manager;

    public UsersRepository(
      ErrorCenterDbContext context,
      UserManager<User> manager
    ) {
      Context = context;
      Manager = manager;
    }

    public async Task<User> FindByEmail(string email) {
      var user = await Context
        .Users
        .Where(x => x.Email == email)
        .AsNoTracking()
        .FirstOrDefaultAsync();

      return (User)user;
    }

    public async Task<IList<string>> GetRoles(User user) {
      var roles = await Manager.GetRolesAsync(user);
      return roles;
    }

    public async Task<User> Create(User user) {
      Context.Users.Add(user);
      await Context.SaveChangesAsync();
      return user;
    }
  }
}