using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Services.IServices
{
    public interface IAuthenticateUserService
    {
        public Task<SessionResponseDTO> Authenticate(SessionRequestDTO data);
    }
}