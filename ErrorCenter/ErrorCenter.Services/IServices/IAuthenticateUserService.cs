using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Services.IServices
{
    public interface IAuthenticateUserService
    {
        public Task<Session> Authenticate(string email, string password);
    }
}