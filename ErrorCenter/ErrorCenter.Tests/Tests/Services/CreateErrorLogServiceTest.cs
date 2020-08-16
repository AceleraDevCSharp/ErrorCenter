using System;

using Moq;
using Xunit;
using AutoMapper;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.DTOs;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class CreateErrorLogServiceTest
    {
        private Mock<IUsersRepository> usersRepository;
        private Mock<IErrorLogRepository<ErrorLog>> errorLogsRepository;
        private Mock<IEnvironmentsRepository> environmentsRepository;
        private Mock<IMapper> mapper;
        private IErrorLogService service;

        public CreateErrorLogServiceTest()
        {
            usersRepository = new Mock<IUsersRepository>();
            errorLogsRepository = new Mock<IErrorLogRepository<ErrorLog>>();
            environmentsRepository = new Mock<IEnvironmentsRepository>();
            mapper = new Mock<IMapper>();
            service = new ErrorLogService(
                usersRepository.Object,
                environmentsRepository.Object,
                errorLogsRepository.Object,
                mapper.Object
            );
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