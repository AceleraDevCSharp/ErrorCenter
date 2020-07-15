using System.Threading.Tasks;

using ErrorCenter.Services.Models;

namespace ErrorCenter.Persistence.EF.Repositories {
  public interface IErrorLogsRepository {
    public Task<ErrorLog> Create(ErrorLog errorLog);
    public Task<ErrorLog> FindById(int id);
    public Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog);
  }
}