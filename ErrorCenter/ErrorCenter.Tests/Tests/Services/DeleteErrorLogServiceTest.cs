using System;

using Moq;
using Xunit;
using AutoMapper;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class DeleteErrorLogServiceTest
    {
        private Mock<IUsersRepository> usersRepository;
        private Mock<IEnvironmentsRepository> environmentsRepository;
        private Mock<IErrorLogRepository<ErrorLog>> errorLogsRepository;
        private Mock<IMapper> mapper;
        private IErrorLogService service;
        
        public DeleteErrorLogServiceTest()
        {
            usersRepository = new Mock<IUsersRepository>();
            errorLogsRepository = new Mock<IErrorLogRepository<ErrorLog>>();

            service = new ErrorLogService(
              usersRepository.Object,
              environmentsRepository.Object,
              errorLogsRepository.Object,
              mapper.Object
            );
        }

        [Fact]
        public async void Should_Be_Able_To_Delete_An_Error_Log()
        {
            // Arrange
            var user = new User()
            {
                Email = "johndoe@example.com",
                UserName = "johndoe@example.com",
                EmailConfirmed = true,
            };

            var errorLog = new ErrorLog()
            {
                Id = 1,
                Environment = new Persistence.EF.Models.Environment()
                {
                    Name = "Development",
                    NormalizedName = "DEVELOPMENT"
                },
                CreatedAt = DateTime.Now,
                ArquivedAt = null,
            };

            // Act
            await usersRepository.Create(user, "Development");
            await errorLogsRepository.Create(errorLog);

            var Deleted = await service.DeleteErrorLog(1, user.Email, "Development");

            // Assert
            Assert.NotNull(Deleted.DeletedAt);
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Delete_Error_If_User_Does_Not_Exist()
        {
            // Arrange

            // Act

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.DeleteErrorLog(
                1,
                "non.existing@example.com",
                "AnyEnvironment"
              )
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Delete_Error_If_Does_Not_Exist()
        {
            // Arrange
            var user = new User()
            {
                Email = "johndoe@example.com",
                UserName = "johndoe@example.com",
                EmailConfirmed = true,
            };

            // Act
            await usersRepository.Create(user, "Development");

            // Assert
            await Assert.ThrowsAsync<ErrorLogException>(
              () => service.DeleteErrorLog(1, user.Email, "AnyRole")
            );
        }

        [Fact]
        public async void Should_Not_Able_To_Delete_Error_If_Not_Same_Environment()
        {
            // Arrange
            var user = new User()
            {
                Email = "johndoe@example.com",
                UserName = "johndoe@example.com",
                EmailConfirmed = true,
            };

            var errorLog = new ErrorLog()
            {
                Id = 1,
                Environment = new Persistence.EF.Models.Environment()
                {
                    Name = "Development",
                    NormalizedName = "DEVELOPMENT"
                },
                CreatedAt = DateTime.Now,
                ArquivedAt = null,
            };

            // Act
            await usersRepository.Create(user, "Development");
            await errorLogsRepository.Create(errorLog);

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => service.DeleteErrorLog(1, user.Email, "DifferentEnvironment")
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Delete_Deleted_Error()
        {
            // Arrange
            var user = new User()
            {
                Email = "johndoe@example.com",
                UserName = "johndoe@example.com",
                EmailConfirmed = true,
            };

            var errorLog = new ErrorLog()
            {
                Id = 1,
                Environment = new Persistence.EF.Models.Environment()
                {
                    Name = "Development",
                    NormalizedName = "DEVELOPMENT"
                },
                CreatedAt = DateTime.Now,
                DeletedAt = DateTime.Now,
            };

            // Act
            await usersRepository.Create(user, "Development");
            await errorLogsRepository.Create(errorLog);

            // Assert
            await Assert.ThrowsAsync<ErrorLogException>(
              () => service.DeleteErrorLog(1, user.Email, "Development")
            );
        }
    }
}