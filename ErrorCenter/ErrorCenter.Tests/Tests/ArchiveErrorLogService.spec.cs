using System;

using Moq;
using Xunit;
using AutoMapper;

using Models = ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;

namespace ErrorCenter.UnitTests
{
    public class ArchiveErrorLogServiceTest
    {
        private Mock<IUsersRepository> usersRepository;
        private Mock<IErrorLogRepository<ErrorLog>> errorLogsRepository;
        private Mock<IEnvironmentsRepository> environmentsRepository;
        private Mock<IMapper> mapper;
        private IErrorLogService service;

        public ArchiveErrorLogServiceTest()
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
        public async void Should_Be_Able_To_Archive_An_Error_Log()
        {
            // Arrange
            var id = 1;
            var email = "johndoe@example.com";
            var role = "SomeRole";
            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();
            errorLog.Environment.Name = role;

            usersRepository.Setup(x => x.FindByEmail(email)).ReturnsAsync(
                UserMock.UserFaker()
            );
            errorLogsRepository.Setup(x => x.FindById(id)).ReturnsAsync(
                errorLog
            );
            errorLogsRepository.Setup(x => x.UpdateErrorLog(errorLog))
                .ReturnsAsync(errorLog);

            // Act
            var archived = await service.ArchiveErrorLog(id, email, role);

            // Assert
            Assert.NotNull(archived.ArquivedAt);
            Console.WriteLine(archived.ArquivedAt);
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Archive_Error_If_User_Does_Not_Exist()
        {
            // Arrange
            var id = 1;
            var email = "non.existing@example.com";
            var role = "AnyEnvironment";
            
            usersRepository.Setup(x => x.FindByEmail(email)).ReturnsAsync(
                (Models.User)null
            );

            // Act

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.ArchiveErrorLog(id, email, role)
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Archive_Error_If_Does_Not_Exist()
        {
            // Arrange
            var id = 1;
            var email = "johndoe@example.com";
            var role = "SomeRole";
            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();
            errorLog.Environment.Name = role;

            usersRepository.Setup(x => x.FindByEmail(email)).ReturnsAsync(
                UserMock.UserFaker()
            );
            errorLogsRepository.Setup(x => x.FindById(id)).ReturnsAsync(
                (Models.ErrorLog)null
            );

            // Act

            // Assert
            await Assert.ThrowsAsync<ErrorLogException>(
              () => service.ArchiveErrorLog(id, email, role)
            );
        }

        [Fact]
        public async void Should_Not_Able_To_Archive_Error_If_Not_Same_Environment()
        {
            // Arrange
            var id = 1;
            var email = "johndoe@example.com";
            var role = "SomeRole";
            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();
            errorLog.Environment.Name = "DifferentRole";

            usersRepository.Setup(x => x.FindByEmail(email)).ReturnsAsync(
                UserMock.UserFaker()
            );
            errorLogsRepository.Setup(x => x.FindById(id)).ReturnsAsync(
                errorLog
            );

            // Act

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.ArchiveErrorLog(id, email, role)
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Archive_Archived_Error()
        {
            // Arrange
            var id = 1;
            var email = "johndoe@example.com";
            var role = "SomeRole";
            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();
            errorLog.Environment.Name = role;
            errorLog.ArquivedAt = DateTime.Now;

            usersRepository.Setup(x => x.FindByEmail(email)).ReturnsAsync(
                UserMock.UserFaker()
            );
            errorLogsRepository.Setup(x => x.FindById(id)).ReturnsAsync(
                errorLog
            );

            // Assert
            await Assert.ThrowsAsync<ErrorLogException>(
              () => service.ArchiveErrorLog(id, email, role)
            );
        }
    }
}