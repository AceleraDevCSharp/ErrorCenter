using System;
using System.IO;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.Services.Fakes;
using ErrorCenter.Services.Providers.HashProvider.Fakes;
using ErrorCenter.Services.Errors;

namespace ErrorCenter.Tests.UnitTests {
  public class AuthenticateUserServiceTest {
    private readonly IUsersRepository usersRepository;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly IConfiguration configuration;
    private readonly IAuthenticateUserService authenticateUserService;

    public AuthenticateUserServiceTest() {
      usersRepository = new FakeUsersRepository();
      passwordHasher = new FakeHashProvider();

      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

      configuration = builder.Build();

      authenticateUserService = new AuthenticateUserService(
        usersRepository,
        passwordHasher,
        configuration
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Authenticate_User() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
        PasswordHash = "password-123"
      };
      await usersRepository.Create(user, "user-role");

      var handler = new JwtSecurityTokenHandler();
      var validationParameters = new TokenValidationParameters() {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
          Encoding.ASCII.GetBytes(configuration["JWTSecret"])
        ),
        ValidateIssuer = false,
        ValidateAudience = false
      };

      // Act
      var session = await authenticateUserService.Authenticate(
        "johndoe@example.com",
        "password-123"
      );
      
      handler.ValidateToken(session.Token, validationParameters, out var validToken);

      // Assert
      Assert.Equal("johndoe@example.com", session.Email);
      //Console.WriteLine(validToken.ValidFrom.Date.ToLocalTime().Date);
      //Console.WriteLine(validToken.ValidTo.Date.ToLocalTime().Date);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_Non_Existing_User() {
      // Arrange
      var user = new User() {
        Email = "johndoe@example.com",
        UserName = "johndoe@example.com",
        EmailConfirmed = true,
        PasswordHash = "password-123"
      };
      await usersRepository.Create(user, "user-role");

      // Act

      // Assert
      await Assert.ThrowsAsync<AuthenticationException>(
        () => authenticateUserService.Authenticate(
          "invalid.user@example.com",
          "password-123"
        )
      );
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Authenticate_User_With_Incorrect_Password() {
      // Arrange

      // Act

      // Assert
      await Assert.ThrowsAsync<AuthenticationException>(
        () => authenticateUserService.Authenticate(
          "johndoe@example.com",
          "wrong-password"
        )
      );
    }
  }
}