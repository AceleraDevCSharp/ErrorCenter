using System;
using Bogus;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.UnitTests.Mocks
{
    public static class UserMock {
        public static User UserFaker() {
            var user = new Faker<User>()
              .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
              .RuleFor(x => x.Email, (f) => f.Internet.Email())
              .RuleFor(x => x.PasswordHash, (f) => f.Internet.Password())
              .RuleFor(x => x.Avatar, () => "default.png");

            return user.Generate();
        }

        public static UserDTO UserDTOFaker() {
            var userDTO = new Faker<UserDTO>()
              .RuleFor(x => x.Email, (f) => f.Internet.Email())
              .RuleFor(x => x.Password, (f) => f.Internet.Password())
              .RuleFor(x => x.Environment, (f) => f.Lorem.Word());

            return userDTO.Generate();
        }
    }
}
