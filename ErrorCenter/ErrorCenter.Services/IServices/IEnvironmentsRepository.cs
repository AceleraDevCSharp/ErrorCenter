using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.IServices {
  public interface IEnvironmentsRepository {
    public Task<Environment> FindByName(string name);
  }
}
