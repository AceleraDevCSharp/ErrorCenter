using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Services.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IEnvironmentsRepository environmentsRepository;
        private readonly IErrorLogRepository<ErrorLog> errorLogRepository;

        public ErrorLogService(
          IUsersRepository usersRepository,
          IEnvironmentsRepository environmentsRepository,
          IErrorLogRepository<ErrorLog> errorLogRepository
        )
        {
            this.usersRepository = usersRepository;
            this.environmentsRepository = environmentsRepository;
            this.errorLogRepository = errorLogRepository;

        }
        public async Task<ErrorLog> CreateNewErrorLog(ErrorLogDTO newErrorLog, string email)
        {
            var user = await usersRepository.FindByEmail(email);

            var user_role = await usersRepository.GetUserRoles(user);

            if (user_role == null)
                throw new EnvironmentException("Environment not found", 404);

            if (!user_role.Contains(newErrorLog.Environment))
            {
                throw new UserException(
                    "User can't create an Error Log of a different environment",
                    StatusCodes.Status403Forbidden
                );
            }
            var environment = await environmentsRepository.FindByName(newErrorLog.Environment);

            var errorLog = new ErrorLog()
            {
                EnvironmentID = environment.Id,
                Level = newErrorLog.Level,
                Title = newErrorLog.Title,
                Details = newErrorLog.Details,
                Origin = newErrorLog.Origin,
                IdUser = user.Id
            };

            errorLog = await errorLogRepository.Create(errorLog);

            return errorLog;

        }
        public async Task<ErrorLog> ArchiveErrorLog(int id, string user_email, string user_role)
        {
            var user = await usersRepository
              .FindByEmail(user_email);

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
                  "User can't archive an Error Log of a different environment",
                  StatusCodes.Status403Forbidden
                );
            }

            if (errorLog.ArquivedAt != null)
            {
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

            if (errorLog.DeletedAt != null)
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
