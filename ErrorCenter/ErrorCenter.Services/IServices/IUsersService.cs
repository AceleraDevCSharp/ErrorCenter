using System.Threading.Tasks;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices {
  public interface IUsersService {
    public Task<User> CreateNewUser(UserDTO newUser);
  }
}
