using Bogus;
using AutoBogus;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.UnitTests.Mocks
{
    public static class UserMock {
        public static User UserFaker() {
          var user = new Faker<User>()
            .RuleFor(x => x.Email, (f) => f.Internet.Email())
            .RuleFor(x => x.PasswordHash, (f) => f.Internet.Password());

          return user.Generate();
        }
    }
}
