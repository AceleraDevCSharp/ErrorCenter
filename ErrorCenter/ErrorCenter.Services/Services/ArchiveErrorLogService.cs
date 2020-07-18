using System;
using System.Threading.Tasks;

using ErrorCenter.Services.Errors;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.Services.Services
{
    public class ArchiveErrorLogService : IArchiveErrorLogService
    {
        private IUsersRepository _usersRepository;
        private IErrorLogRepository<ErrorLog> _errorLogRepository;

        public ArchiveErrorLogService(
          IUsersRepository usersRepository,
          IErrorLogRepository<ErrorLog> errorLogsRepository
        )
        {
            _usersRepository = usersRepository;
            _errorLogRepository = errorLogsRepository;
        }

        public async Task<ErrorLog> Execute(int id, string user_email)
        {
            var user = await _usersRepository.FindByEmail(user_email);

            if (user == null) throw new UserNotFoundException();

            var errorLog = await _errorLogRepository.FindById(id);

            if (errorLog == null) throw new ErrorLogNotFoundException();

            if (errorLog.ArquivedAt != null) throw new ErrorLogArchivedException();

            if (!user.Environment.Equals(errorLog.Environment))
                throw new DifferentEnvironmentException();

            errorLog.ArquivedAt = DateTime.Now;

            var archivedError = await _errorLogRepository.UpdateErrorLog(errorLog);

            return archivedError;
        }
    }
}
