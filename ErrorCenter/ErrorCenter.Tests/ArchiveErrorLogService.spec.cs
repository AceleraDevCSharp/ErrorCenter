using System;
using Xunit;

using ErrorCenter.Domain;
using ErrorCenter.Services;
using ErrorCenter.Services.Errors;
using ErrorCenter.Persistence.EF.Repositories;
using ErrorCenter.Persistence.EF.Repositories.Fakes;

namespace ErrorCenter.Tests.Services {
  public class ArchiveErrorLogSerivceTest {
    private IUsersRepository _usersRepository;
    private IErrorLogsRepository _errorLogsRepository;
    private ArchiveErrorLogSerivce _serivce;
    public ArchiveErrorLogSerivceTest() {
      _usersRepository = new FakeUsersRepository();
      _errorLogsRepository = new FakeErrorLogsRepository();

      _serivce = new ArchiveErrorLogSerivce(
        _usersRepository,
        _errorLogsRepository
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Archive_An_Error_Log() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      var errorLog = new ErrorLog();
      errorLog.Id = 1;
      errorLog.IdUser = 1;
      errorLog.Environment = "dev";
      errorLog.CreatedAt = DateTime.Now;

      await _usersRepository.Create(user);
      await _errorLogsRepository.Create(errorLog);

      var archived = await _serivce.Execute(1, user.Email);

      Assert.NotNull(archived.ArquivedAt);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Error_If_User_Does_Not_Exist() {
      await Assert.ThrowsAsync<UserNotFoundException>(
        () => _serivce.Execute(1, "non.existing@example.com")
      );
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Error_If_Does_Not_Exist() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      await _usersRepository.Create(user);

      await Assert.ThrowsAsync<ErrorLogNotFoundException>(
        () => _serivce.Execute(1, user.Email)
      );
    }

    [Fact]
    public async void Should_Not_Able_To_Archive_Error_If_Not_Same_Environment() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      var errorLog = new ErrorLog();
      errorLog.Id = 1;
      errorLog.IdUser = 1;
      errorLog.Environment = "prod";
      errorLog.CreatedAt = DateTime.Now;

      await _usersRepository.Create(user);
      await _errorLogsRepository.Create(errorLog);

      await Assert.ThrowsAsync<DifferentEnvironmentException>(
        () => _serivce.Execute(1, user.Email)
      );
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Archived_Error() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      var errorLog = new ErrorLog();
      errorLog.Id = 1;
      errorLog.IdUser = 1;
      errorLog.Environment = "dev";
      errorLog.CreatedAt = DateTime.Now;
      errorLog.ArquivedAt = DateTime.Now;

      await _usersRepository.Create(user);
      await _errorLogsRepository.Create(errorLog);

      await Assert.ThrowsAsync<ErrorLogArchivedException>(
        () => _serivce.Execute(1, user.Email)
      );
    }
  }
}