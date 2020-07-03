using System.Threading.Tasks;

using ErrorCenter.Domain;

namespace ErrorCenter.Persistence.EF.Repositories {
  public interface IErrorLogsRepository {
    public Task<ErrorLog> Create(ErrorLog errorLog);
    public Task<ErrorLog> FindById(int id);
    public Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog);
  }
}