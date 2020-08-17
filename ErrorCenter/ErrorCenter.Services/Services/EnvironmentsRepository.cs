using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Services
{
    public class EnvironmentsRepository : IEnvironmentsRepository
    {
        private readonly RoleManager<Environment> roleManager;

        public EnvironmentsRepository(RoleManager<Environment> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<Environment> FindByName(string name)
        {
            var environment = await roleManager.FindByNameAsync(name);

            return environment;
        }

    }
}
