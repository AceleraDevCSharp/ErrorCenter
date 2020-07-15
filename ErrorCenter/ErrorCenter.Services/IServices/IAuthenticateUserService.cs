using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IAuthenticateUserService
    {
        public Task<Session> Execute(string email, string password);
    }
}