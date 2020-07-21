using System;
using System.Threading.Tasks;

using ErrorCenter.Services.Errors;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.IRepository;
using ErrorCenter.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace ErrorCenter.Services.Services {
  public class ArchiveErrorLogService : IArchiveErrorLogService {
    private IUsersRepository _usersRepository;
    private IErrorLogRepository<ErrorLog> _errorLogRepository;

    public ArchiveErrorLogService(
      IUsersRepository usersRepository,
      IErrorLogRepository<ErrorLog> errorLogsRepository
    ) {
      _usersRepository = usersRepository;
      _errorLogRepository = errorLogsRepository;
    }

    public async Task<ErrorLog> Execute(int id, string user_email) {
      var user = await _usersRepository.FindByEmail(user_email);

      if (user == null) {
        throw new UserException(
          "Requesting user is no longer valid",
          StatusCodes.Status401Unauthorized
        );
      }

      var errorLog = await _errorLogRepository.FindById(id);

      if (errorLog == null) {
        throw new ErrorLogException(
          "Error Log not found",
          StatusCodes.Status404NotFound
        );
      }

      if (errorLog.ArquivedAt != null) {
        throw new ErrorLogException(
          "Requested Error Log is already archived",
          StatusCodes.Status400BadRequest
        );
      }

      /*
      if (!user.Environment.Equals(errorLog.Environment)) {
        throw new UserException(
          "User can't archive an Error Log of a different environment",
          StatusCodes.Status403Forbidden
        );
      }
      */

      errorLog.ArquivedAt = DateTime.Now;

      var archivedError = await _errorLogRepository.UpdateErrorLog(errorLog);

      return archivedError;
    }
  }
}
