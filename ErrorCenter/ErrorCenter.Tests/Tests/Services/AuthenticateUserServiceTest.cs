using System.IO;
using System.Text;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class AuthenticateUserServiceTest
    {
        private readonly Mock<IUsersRepository> usersRepository;
        private readonly Mock<IPasswordHasher<User>> passwordHasher;
        private readonly IConfiguration configuration;
        private readonly IAuthenticateUserService authenticateUserService;

        public AuthenticateUserServiceTest()
        {
            usersRepository = new Mock<IUsersRepository>();
            passwordHasher = new Mock<IPasswordHasher<User>>();

            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
              .AddEnvironmentVariables();

            configuration = builder.Build();

            authenticateUserService = new AuthenticateUserService(
              usersRepository.Object,
              passwordHasher.Object,
              configuration
            );
        }

        [Fact]
        public async void Should_Be_Able_To_Authenticate_User()
        {
            // Arrange
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration["JWTSecret"])
              ),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var data = new SessionRequestDTO()
            {
                Email = "johndoe@example.com",
                Password = "password-123"
            };

            var fakeUser = UserMock.UserFaker();

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
                fakeUser
            );
            passwordHasher.Setup(x => x.VerifyHashedPassword(
                fakeUser,
                fakeUser.PasswordHash,
                "password-123"
            ))
            .Returns(PasswordVerificationResult.Success);
            usersRepository.Setup(x => x.GetUserRoles(fakeUser)).ReturnsAsync(
                new List<string>() { "Development" }
            );

            // Act
            var session = await authenticateUserService.Authenticate(data);

            handler.ValidateToken(
                session.Token,
                validationParameters,
                out var validToken
            );

            // Assert
            Assert.Equal(fakeUser.Email, session.Email);
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Authenticate_Non_Existing_User()
        {
            // Arrange
            var data = new SessionRequestDTO()
            {
                Email = "invalid.user@example.com",
                Password = "password-123"
            };

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
                (User)null
            );

            // Act

            // Assert
            await Assert.ThrowsAsync<AuthenticationException>(
              () => authenticateUserService.Authenticate(data)
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Authenticate_User_With_Incorrect_Password()
        {
            // Arrange
            var data = new SessionRequestDTO()
            {
                Email = "johndoe@example.com",
                Password = "wrong-password"
            };

            var fakeUser = UserMock.UserFaker();

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
                fakeUser
            );
            passwordHasher.Setup(x => x.VerifyHashedPassword(
                fakeUser,
                fakeUser.PasswordHash,
                data.Password
            ))
            .Returns(PasswordVerificationResult.Failed);

            // Act

            // Assert
            await Assert.ThrowsAsync<AuthenticationException>(
              () => authenticateUserService.Authenticate(data)
            );
        }
    }
}