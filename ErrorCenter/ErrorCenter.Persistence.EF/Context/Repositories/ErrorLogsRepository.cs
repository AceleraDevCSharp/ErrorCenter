using System.Threading.Tasks;

using ErrorCenter.Domain;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.Persistence.EF.Context.Repositories {
  public class ErrorLogsRepository : IErrorLogsRepository {
    private ErrorCenterDbContext Context;

    public ErrorLogsRepository(ErrorCenterDbContext context) {
      Context = context;
    }

    public async Task<ErrorLog> Create(ErrorLog errorLog) {
      Context.ErrorLogs.Add(errorLog);
      await Context.SaveChangesAsync();
      return errorLog;
    }

    public async Task<ErrorLog> FindById(int id) {
      var errorLog = await Context.ErrorLogs.FindAsync(id);
      return errorLog;
    }

    public async Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog) {
      Context.Update(errorLog);
      await Context.SaveChangesAsync();
      return errorLog;
    }
  }
}
