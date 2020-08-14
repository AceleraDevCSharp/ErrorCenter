using AutoBogus;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.WebAPI.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ErrorCenter.Tests.UnitTests.Mocks
{
    public static class ErrorLogMock
    {
        public static IEnumerable<ErrorLog> ErrorLogModelFaker =>
            AutoFaker.Generate<ErrorLog>(3);

        public static IEnumerable<ErrorLogViewModel> ErrorLogViewModelFaker =>
            AutoFaker.Generate<ErrorLogViewModel>(3);

        public static IEnumerable<Persistence.EF.Models.Environment> EnviromentsFaker => new List<Persistence.EF.Models.Environment>
        {
            new Persistence.EF.Models.Environment{ Name = "Development" },
            new Persistence.EF.Models.Environment{ Name = "Homologation" },
            new Persistence.EF.Models.Environment{ Name = "Production" }
        };
    }
}
