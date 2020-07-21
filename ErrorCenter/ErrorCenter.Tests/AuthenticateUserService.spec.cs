using System;
using System.IO;
using Xunit;
using Microsoft.Extensions.Configuration;
using ErrorCenter.Persistence.EF.Repositories.Fakes;
using ErrorCenter.Services.Providers.HashProvider.Fakes;
using ErrorCenter.Services.Providers.HashProvider.Models;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services;
using ErrorCenter.Persistence.EF.IRepository;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.Tests.Services
{
    public class AuthenticateUserServiceTest {
    private IUsersRepository _usersRepository;
    private IHashProvider _hashProvider;
    private IConfiguration _config;
    private IAuthenticateUserService _service;

    public AuthenticateUserServiceTest() {
      _usersRepository = new FakeUsersRepository();
      _hashProvider = new FakeHashProvider();

      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

      _config = builder.Build();

      _service = new AuthenticateUserService(
        _usersRepository,
        _hashProvider,
        _config
      );
    }
    /*
    [Fact]
    public async void Should_Be_Able_To_Authenticate_User() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      await _usersRepository.Create(user);

      var response = await _service.Execute("johndoe@example.com", "123456");

      Assert.Equal("johndoe@example.com", response.Email);
      Assert.Equal("dev", response.Environment);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_Non_Existing_User() {
      var response = await _service.Execute("johndoe@example.com", "123456");
      Assert.Null(response);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_User_With_Incorrect_Password() {
      var user = new User();
      user.Id = 1;
      user.Email = "johndoe@example.com";
      user.Password = "123456";
      user.Environment = "dev";
      user.CreatedAt = DateTime.Now;

      await _usersRepository.Create(user);

      var response = await _service.Execute("johndoe@example.com", "incorrect-password");

      Assert.Null(response);
    }
    */
  }
}