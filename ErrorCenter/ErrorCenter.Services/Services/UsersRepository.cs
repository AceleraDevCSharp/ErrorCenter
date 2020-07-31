using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Services.Services {
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

      return user;
    }

    public async Task<User> FindByEmailTracking(string email) {
      var user = await Context
      .Users
      .Where(x => x.Email == email)
      .FirstOrDefaultAsync();

      return user;
    }

    public async Task<IList<string>> GetRoles(User user) {
      var roles = await Manager.GetRolesAsync(user);
      return roles;
    }

    public async Task<User> Create(User user, string role) {
      await Manager.CreateAsync(user);
      await Manager.AddToRoleAsync(user, role);

      return user;
    }

    public async Task<User> Save(User user) {
      await Manager.UpdateAsync(user);

      return user;
    }

    public async Task<IList<string>> GetUserRoles(User user) {
      var userRoles = await Manager.GetRolesAsync(user);

      return userRoles;
    }
  }
}