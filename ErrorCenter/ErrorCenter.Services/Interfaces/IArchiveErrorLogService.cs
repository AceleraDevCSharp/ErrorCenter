using System.Threading.Tasks;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Interfaces {
  public interface IArchiveErrorLogService {
    public Task<ErrorLog> Execute(int id, string user_email);

  }
}