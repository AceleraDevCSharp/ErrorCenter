using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using Environment = ErrorCenter.Persistence.EF.Models.Environment;

namespace ErrorCenter.Services.Services.Fakes
{
    public class FakeEnvironmentsRepository : IEnvironmentsRepository
    {

        private readonly List<Environment> environments = new List<Persistence.EF.Models.Environment>();

        public FakeEnvironmentsRepository()
        {
            environments = new List<Persistence.EF.Models.Environment>();
        }


        public async Task<Environment> FindByName(string name)
        {
            var environment = environments.Find(x => x.Name == name);

            await Task.Delay(1);

            return environment;
        }
    }
}