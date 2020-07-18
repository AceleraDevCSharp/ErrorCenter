using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.Services.Services
{
    public class UsersRepository : IUsersRepository
    {
        private ErrorCenterDbContext Context;

        public UsersRepository(ErrorCenterDbContext context)
        {
            Context = context;
        }

        public async Task<User> FindByEmail(string email)
        {
            var user = await Context
              .Users
              .Where(x => x.Email == email)
              .AsNoTracking()
              .FirstOrDefaultAsync();
            return user;
        }
    }
}