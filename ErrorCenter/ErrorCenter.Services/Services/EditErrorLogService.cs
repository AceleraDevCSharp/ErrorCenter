using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Services.Services {
  public class EditErrorLogService : IEditErrorLogService {
        private readonly IUsersRepository usersRepository;
        private readonly IGetErrorLogService<ErrorLog> errorLogRepository;

        public EditErrorLogService(
          IUsersRepository usersRepository,
          IGetErrorLogService<ErrorLog> errorLogRepository
        ) {
          this.usersRepository = usersRepository;
          this.errorLogRepository = errorLogRepository;
        }

        public async Task<ErrorLog> ArchiveErrorLog(int id, string user_email, string user_role) {

          var user = await usersRepository
            .FindByEmail(user_email);

          if (user == null) {
            throw new UserException(
              "Requesting user is no longer valid",
              StatusCodes.Status401Unauthorized
            );
          }

          var errorLog = await errorLogRepository.FindById(id);

          if (errorLog == null) {
            throw new ErrorLogException(
              "Error Log not found",
              StatusCodes.Status404NotFound
            );
          }

          if (!user_role.Equals(errorLog.Environment.Name)) {
            throw new UserException(
              "User can't archive an Error Log of a different environment",
              StatusCodes.Status403Forbidden
            );
          }

          if (errorLog.ArquivedAt != null) {
            throw new ErrorLogException(
              "Requested Error Log is already archived",
              StatusCodes.Status400BadRequest
            );
          }

          errorLog.ArquivedAt = DateTime.Now;

          var archivedError = await errorLogRepository.UpdateErrorLog(errorLog);

          return archivedError;
        }

        public async Task<ErrorLog> DeleteErrorLog(int id, string user_email, string user_role)
        {
            var user = await usersRepository.FindByEmail(user_email);

            if (user == null)
            {
                throw new UserException(
                  "Requesting user is no longer valid",
                  StatusCodes.Status401Unauthorized
                );
            }

            var errorLog = await errorLogRepository.FindById(id);

            if (errorLog == null)
            {
                throw new ErrorLogException(
                  "Error Log not found",
                  StatusCodes.Status404NotFound
                );
            }

            if (!user_role.Equals(errorLog.Environment.Name))
            {
                throw new UserException(
                  "User can't delete an Error Log of a different environment",
                  StatusCodes.Status403Forbidden
                );
            }

            if (errorLog.ArquivedAt != null)
            {
                throw new ErrorLogException(
                  "Requested Error Log is already deleted",
                  StatusCodes.Status400BadRequest
                );
            }

            errorLog.DeletedAt = DateTime.Now;

            var archivedError = await errorLogRepository.UpdateErrorLog(errorLog);

            return archivedError;
        }
    }
  }

