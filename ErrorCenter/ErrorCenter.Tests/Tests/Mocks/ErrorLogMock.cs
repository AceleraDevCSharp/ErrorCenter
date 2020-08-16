using System;
using System.Collections.Generic;

using Bogus;
using AutoBogus;

using ErrorCenter.WebAPI.ViewModel;
using Models = ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.UnitTests.Mocks
{
    public static class ErrorLogMock
    {
        public static Models.ErrorLog SingleErrorLogModelFaker() {
            var errorLog = new Faker<Models.ErrorLog>()
                .RuleFor(x => x.Id, (f) => f.Random.Int(1))
                .RuleFor(x => x.Level, (f) => f.Lorem.Word())
                .RuleFor(x => x.Title, (f) => f.Lorem.Word())
                .RuleFor(x => x.Details, (f) => f.Lorem.Sentences(1))
                .RuleFor(x => x.Quantity, (f) => f.Random.Int(1))
                .RuleFor(x => x.CreatedAt, () => DateTime.Now)
                .RuleFor(x => x.ArquivedAt, () => null)
                .RuleFor(x => x.DeletedAt, () => null)
                .RuleFor(x => x.Origin, (f) => f.Lorem.Word())
                .RuleFor(x => x.EnvironmentID, () => Guid.NewGuid().ToString())
                .RuleFor(x => x.Environment, (f, u) => new Models.Environment() {
                    Id = u.EnvironmentID,
                    Name = f.Lorem.Word(),
                });

            return errorLog.Generate();
        } 
        public static IEnumerable<Models.ErrorLog> ErrorLogModelFaker =>
            AutoFaker.Generate<Models.ErrorLog>(3);

        public static IEnumerable<ErrorLogViewModel> ErrorLogViewModelFaker =>
            AutoFaker.Generate<ErrorLogViewModel>(3);

        public static IEnumerable<Persistence.EF.Models.Environment> EnviromentsFaker => new List<Persistence.EF.Models.Environment>
        {
            new Models.Environment{ Name = "Development" },
            new Models.Environment{ Name = "Homologation" },
            new Models.Environment{ Name = "Production" }
        };
    }
}
