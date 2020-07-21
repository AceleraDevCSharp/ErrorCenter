using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.IRepository;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Persistence.EF.Repository
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

            return (User)user;
        }

        public async Task<User> Create(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
            return user;
        }
    }
}