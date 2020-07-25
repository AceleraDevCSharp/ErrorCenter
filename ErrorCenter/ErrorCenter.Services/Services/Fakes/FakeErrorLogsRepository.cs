using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Services.Fakes {
  public class FakeErrorLogsRepository : IErrorLogRepository<ErrorLog> {
    private List<ErrorLog> errorLogs = new List<ErrorLog>();

    public FakeErrorLogsRepository() {
      errorLogs = new List<ErrorLog>();
    }

    public async Task<ErrorLog> Create(ErrorLog errorLog) {
      errorLogs.Add(errorLog);
      
      await Task.Delay(1);
      
      return errorLog;
    }

    public async Task<ErrorLog> FindById(int id) {
      var errorLog = errorLogs.Find(x => x.Id == id);

      await Task.Delay(1);
      
      return errorLog;
    }

    public async Task<IEnumerable<Persistence.EF.Models.Environment>> Environments() {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectAll() {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectArchived() {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectByEnvironment(string whereEnvironment = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBy(string whereEnvironment = null, string orderby = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectByEnvironmentOrderedBySearchBy(string whereEnvironment = null, string orderby = null, string whereSearch = null, string searchText = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectByEnvironmentSearchBy(string whereEnvironment = null, string whereSearch = null, string searchText = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectDeleted() {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectOrderedBy(string orderby = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectOrderedBySearchBy(string orderby = null, string whereSearch = null, string searchText = null) {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ErrorLog>> SelectSearchBy(string whereSearch = null, string searchText = null) {
      throw new System.NotImplementedException();
    }

    public async Task<ErrorLog> UpdateErrorLog(ErrorLog errorLog) {
      var idx = errorLogs.FindIndex(x => x.Id == errorLog.Id);
      errorLogs[idx] = errorLog;
      await Task.Delay(10);
      return errorLog;
    }
  }
}