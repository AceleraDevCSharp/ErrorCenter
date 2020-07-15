using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Persistence.EF.Repositories.Fakes {
  public class FakeErrorLogsRepository : IErrorLogsRepository {
    private List<ErrorLog> errorLogs = new List<ErrorLog>();

    public FakeErrorLogsRepository() {
      errorLogs = new List<ErrorLog>();
    }

    public async Task<ErrorLog> Create(ErrorLog errorLog) {
      errorLogs.Add(errorLog);
      await Task.Delay(10);
      return errorLog;
    }

    public async Task<ErrorLog> FindById(int id) {
      var errorLog = errorLogs.Find(x => x.Id == id);
      await Task.Delay(10);
      return errorLog;
    }

    public async Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog) {
      var idx = errorLogs.FindIndex(x => x.Id == errorLog.Id);
      errorLogs[idx] = errorLog;
      await Task.Delay(10);
      return errorLog;
    }
  }
}