using System.Linq;

using Microsoft.AspNetCore.Identity;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using System;

namespace ErrorCenter.Tests.Utilities {
  public static class SeedDatabase {
    public static void InitializeDb(ErrorCenterDbContext db) {
      var user = new User() {
        Email = "loremipsum@example.com",
        UserName = "loremipsum@example.com",
        EmailConfirmed = true,
      };
      db.Users.Add(user);

      var environmentId = db.Roles
        .Where(x => x.Name.Equals("Development"))
        .FirstOrDefault()
        .Id;

      db.UserRoles.Add(new IdentityUserRole<string>(){
        RoleId = environmentId,
        UserId = user.Id
      });

      var errorLog = new ErrorLog() {
        Level = "Warning",
        Title = "Lorem Ipsum",
        Details = "Lorem Ipsum",
        Origin = "Lorem Ipsum",
        Quantity = 1,
        CreatedAt = DateTime.Now,
        IdUser = user.Id,
        EnvironmentID = environmentId,
      };
      db.ErrorLogs.Add(errorLog);

      db.SaveChanges();
    }
  }
}