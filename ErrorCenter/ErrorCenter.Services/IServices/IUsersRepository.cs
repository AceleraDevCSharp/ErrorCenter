using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IUsersRepository
    {
        public Task<User> FindByEmail(string email);
    }
}