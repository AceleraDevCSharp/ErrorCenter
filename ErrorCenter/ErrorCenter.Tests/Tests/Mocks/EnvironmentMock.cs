using System;

using Bogus;

using Models = ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.UnitTests.Mocks
{
    public static class EnvironmentMock {
        public static Models.Environment EnvironmentFaker() {
            var environments = new Faker<Models.Environment>()
              .RuleFor(x => x.Id, () => Guid.NewGuid().ToString())
              .RuleFor(x => x.Name, (f) => f.Lorem.Word());

            return environments.Generate();
        }
    }
}
