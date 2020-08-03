using System;

using Xunit;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services.Fakes;

namespace ErrorCenter.Tests.UnitTests {
  public class ArchiveErrorLogServiceTest {
    private IUsersRepository usersRepository;
    private IErrorLogRepository<ErrorLog> errorLogsRepository;
    private IErrorLogService service;
    public ArchiveErrorLogServiceTest() {
      usersRepository = new FakeUsersRepository();
      errorLogsRepository = new FakeErrorLogsRepository();

      service = new ArchiveErrorLogService(
        usersRepository,
        errorLogsRepository
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Archive_An_Error_Log() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
      };

      var errorLog = new ErrorLog() {
        Id = 1,
        Environment = new Persistence.EF.Models.Environment() {
          Name = "Development",
          NormalizedName = "DEVELOPMENT"
        },
        CreatedAt = DateTime.Now,
        ArquivedAt = null,
      };

      // Act
      await usersRepository.Create(user, "Development");
      await errorLogsRepository.Create(errorLog);

      var archived = await service.ArchiveErrorLog(1, user.Email, "Development");

      // Assert
      Assert.NotNull(archived.ArquivedAt);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Error_If_User_Does_Not_Exist() {
      // Arrange

      // Act

      // Assert
      await Assert.ThrowsAsync<UserException>(
        () => service.ArchiveErrorLog(
          1,
          "non.existing@example.com",
          "AnyEnvironment"
        )
      );
    }
    
    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Error_If_Does_Not_Exist() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
      };

      // Act
      await usersRepository.Create(user, "Development");

      // Assert
      await Assert.ThrowsAsync<ErrorLogException>(
        () => service.ArchiveErrorLog(1, user.Email, "AnyRole")
      );
    }

    [Fact]
    public async void Should_Not_Able_To_Archive_Error_If_Not_Same_Environment() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
      };

      var errorLog = new ErrorLog() {
        Id = 1,
        Environment = new Persistence.EF.Models.Environment() {
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
        () => service.ArchiveErrorLog(1, user.Email, "DifferentEnvironment")
      );
    }
    
    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Archived_Error() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
      };

      var errorLog = new ErrorLog() {
        Id = 1,
        Environment = new Persistence.EF.Models.Environment() {
          Name = "Development",
          NormalizedName = "DEVELOPMENT"
        },
        CreatedAt = DateTime.Now,
        ArquivedAt = DateTime.Now,
      };

      // Act
      await usersRepository.Create(user, "Development");
      await errorLogsRepository.Create(errorLog);

      // Assert
      await Assert.ThrowsAsync<ErrorLogException>(
        () => service.ArchiveErrorLog(1, user.Email, "Development")
      );
    }
  }
}