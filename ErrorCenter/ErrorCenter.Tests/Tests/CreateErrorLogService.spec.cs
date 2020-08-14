using System;

using Xunit;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services.Fakes;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.UnitTests
{
    public class CreateErrorLogServiceTest
    {
        private IUsersRepository usersRepository;
        private IErrorLogRepository<ErrorLog> errorLogsRepository;
        private IErrorLogService service;
        private IEnvironmentsRepository environmentsRepository;

        public CreateErrorLogServiceTest()
        {
            usersRepository = new FakeUsersRepository();
            errorLogsRepository = new FakeErrorLogsRepository();
            environmentsRepository = new FakeEnvironmentsRepository();
            service = new ErrorLogService(usersRepository, environmentsRepository, errorLogsRepository, null);
        }

        [Fact]
        public async void Should_Be_Able_To_Create_An_Error_Log()
        {

            var errologDTO = new ErrorLogDTO()
            {
                Environment = "Development",
                Details = "Detalhes1",
                Level = "Level1",
                Origin = "Origem1",
                Title = "Titulo1"

            };

            var user = new User()
            {
                Email = "john1@example.com",
                UserName = "john1@example.com",
                EmailConfirmed = true,
            };
            var userEnvironment = errologDTO.Environment;

            await usersRepository.Create(user, errologDTO.Environment);

            var Created = await service.CreateNewErrorLog(errologDTO, user.Email);

            Assert.Equal(Created, errologDTO);

        }

        [Fact]
        public async void Should_Not_Able_To_Create_Error_If_Not_Same_Environment()
        {
            // Arramge
            var errologDTO = new ErrorLogDTO()
            {
                Environment = "Development",
                Details = "Detalhes1",
                Level = "Level1",
                Origin = "Origem1",
                Title = "Titulo1"

            };

            var user = new User()
            {
                Email = "johnOtherEnv@example.com",
                UserName = "johnOtherEnv@example.com",
                EmailConfirmed = true,
            };
            var userResponse = await usersRepository.Create(user, "OtherEnvironment");


            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.CreateNewErrorLog(errologDTO, user.Email)
            );
        }
    }
}