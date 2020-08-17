
using Moq;
using Xunit;
using AutoMapper;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class CreateErrorLogServiceTest
    {
        private Mock<IUsersRepository> usersRepository;
        private Mock<IErrorLogRepository<ErrorLog>> errorLogsRepository;
        private Mock<IEnvironmentsRepository> environmentsRepository;
        private readonly Mock<IPasswordHasher<User>> passwordHasher;
        private Mock<IMapper> mapper;
        private IErrorLogService erroService;

        public CreateErrorLogServiceTest()
        {
            usersRepository = new Mock<IUsersRepository>();
            errorLogsRepository = new Mock<IErrorLogRepository<ErrorLog>>();
            environmentsRepository = new Mock<IEnvironmentsRepository>();
            mapper = new Mock<IMapper>();
            passwordHasher = new Mock<IPasswordHasher<Persistence.EF.Models.User>>();

            erroService = new ErrorLogService(
              usersRepository.Object,
              environmentsRepository.Object,
              errorLogsRepository.Object,
              mapper.Object
            );

        }

        [Fact]
        public async void Should_Create_An_Error_Log()
        {

            // Arrange
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();
            var errolog = ErrorLogMock.SingleErrorLogModelFaker();
            errolog.Environment = environment;

            var errologDTO = ErrorLogMock.SingleErrorLogModelDTO(errolog);

            environmentsRepository.Setup(x => x.FindByName(environment.Name)).ReturnsAsync(
                environment);

            usersRepository.Setup(x => x.FindByEmail(user.Email)).ReturnsAsync(
                user);

            usersRepository.Setup(x => x.GetUserRoles(user)).ReturnsAsync(
                EnvironmentMock.GetFakeEnvironment(environment.Name));

            errorLogsRepository.Setup(x => x.Create(It.IsAny<ErrorLog>())).ReturnsAsync(
                errolog);


            // Act
            var response = await erroService.CreateNewErrorLog(errologDTO, user.Email);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(errolog, response);

        }

        [Fact]
        public async void Should_Not_Create_Error_Log_If_Environment_Not_Exist()
        {
            //Arrange
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();
            var errolog = ErrorLogMock.SingleErrorLogModelFaker();
            errolog.Environment = environment;

            var errologDTO = ErrorLogMock.SingleErrorLogModelDTO(errolog);

            usersRepository.Setup(x => x.FindByEmail(user.Email)).ReturnsAsync(
              (Persistence.EF.Models.User)null);

            environmentsRepository.Setup(x => x.FindByName(errologDTO.Environment)).ReturnsAsync(
                (Persistence.EF.Models.Environment)null);

            usersRepository.Setup(x => x.GetUserRoles(user)).ReturnsAsync(
                (IList<string>)null);

            // Assert
            await Assert.ThrowsAsync<EnvironmentException>(
              () => erroService.CreateNewErrorLog(errologDTO, user.Email)
            );
        }

        [Fact]
        public async void Should_Not_Create_Error_Log_If_Not_Same_Environmentt()
        {
            //Arrange
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();
            var errolog = ErrorLogMock.SingleErrorLogModelFaker();
            errolog.Environment.Name = "DifferentRole";

            var errologDTO = ErrorLogMock.SingleErrorLogModelDTO(errolog);

            usersRepository.Setup(x => x.FindByEmail(user.Email)).ReturnsAsync(
              (Persistence.EF.Models.User)null);

            environmentsRepository.Setup(x => x.FindByName(errologDTO.Environment)).ReturnsAsync(
                (Persistence.EF.Models.Environment)null);

            usersRepository.Setup(x => x.GetUserRoles(user)).ReturnsAsync(
                (IList<string>)null);

            // Assert
            await Assert.ThrowsAsync<EnvironmentException>(
              () => erroService.CreateNewErrorLog(errologDTO, user.Email)
            );
        }
    }
}