using System;
using System.Threading.Tasks;

using ErrorCenter.Services.Models;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Interfaces;

namespace ErrorCenter.Services {
  public class ArchiveErrorLogService : IArchiveErrorLogService {
    private IUsersRepository _usersRepository;
    private IErrorLogsRepository _errorLogsRepository;

    public ArchiveErrorLogService(
      IUsersRepository usersRepository,
      IErrorLogsRepository errorLogsRepository
    ) {
        _usersRepository = usersRepository;
      _errorLogsRepository = errorLogsRepository;
    }

    public async Task<ErrorLog> Execute(int id, string user_email) {
      var user = await _usersRepository.FindByEmail(user_email);

      if (user == null) throw new UserNotFoundException();

      var errorLog = await _errorLogsRepository.FindById(id);

      if (errorLog == null) throw new ErrorLogNotFoundException();

      if (errorLog.ArquivedAt != null) throw new ErrorLogArchivedException();

      if (!user.Environment.Equals(errorLog.Environment))
        throw new DifferentEnvironmentException();

      errorLog.ArquivedAt = DateTime.Now;

      var archivedError = await _errorLogsRepository.UpdateErrorLog(errorLog);

      return archivedError;
    }
  }
}
