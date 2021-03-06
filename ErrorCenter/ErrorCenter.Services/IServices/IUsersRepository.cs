using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IUsersRepository
    {
        public Task<User> Create(User user, string role);
        public Task<User> Save(User user);
        public Task<User> FindByEmail(string email);
        public Task<User> FindByEmailTracking(string email);
        public Task<IList<string>> GetUserRoles(User user);
    }
}