using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices
{
    public interface IAuthenticateUserService
    {
        Task<Session> Execute(string email, string password);
    }
}