using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Services.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private IUsersRepository usersRepository;
        private IEnvironmentsRepository environmentsRepository;
        private IErrorLogRepository<ErrorLog> errorLogRepository;
        private IMapper _mapper;
        public ErrorLogService(
          IUsersRepository usersRepository,
          IEnvironmentsRepository environmentsRepository,
          IErrorLogRepository<ErrorLog> errorLogRepository,
          IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.environmentsRepository = environmentsRepository;
            this.errorLogRepository = errorLogRepository;
            _mapper = mapper;

        }
        public async Task<ErrorLogDTO> CreateNewErrorLog(ErrorLogDTO newErrorLog, string email)
        {
            var user = await usersRepository.FindByEmail(email);

            if (user == null)
            {
                throw new UserException("Requesting user is no longer valid",
                                        StatusCodes.Status401Unauthorized
                );
            }

            var user_role = await usersRepository.GetUserRoles(user);

            if (user_role == null)
            {
                throw new EnvironmentException("Environment not found", 404);
            }

            if (!user_role.Contains(newErrorLog.Environment))
            {
                throw new UserException(
                  "User can't create an Error Log of a different environment",
                  StatusCodes.Status403Forbidden
                );
            }

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
                Details = newErrorLog.Details,
                Level = newErrorLog.Level,
                Origin = newErrorLog.Origin,
                Title = newErrorLog.Title,
                User = user,
                IdUser = user.Id,
                Environment = environment,
                EnvironmentID = environment.Id,
                
            };


            await errorLogRepository.Create(errorLog);

            var errorLogDTO = _mapper.Map<ErrorLogDTO>(errorLog);

            return errorLogDTO;

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
