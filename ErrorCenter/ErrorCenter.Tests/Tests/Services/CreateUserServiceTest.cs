using System.Threading.Tasks;

using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;

using ErrorCenter.Services.Errors;
using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using Models = ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;

namespace ErrorCenter.Tests.UnitTests.Services
{
    public class CreateUserServiceTest
    {
        private readonly Mock<IUsersRepository> usersRepository;
        private readonly Mock<IEnvironmentsRepository> environmentsRepository;
        private readonly Mock<IPasswordHasher<Models.User>> passwordHasher;
        private readonly IUsersService usersService;

        public CreateUserServiceTest()
        {
            this.usersRepository = new Mock<IUsersRepository>();
            this.environmentsRepository = new Mock<IEnvironmentsRepository>();
            this.passwordHasher = new Mock<IPasswordHasher<Models.User>>();
            this.usersService = new UsersService(
                usersRepository.Object,
                environmentsRepository.Object,
                passwordHasher.Object
            );
        }

        [Fact]
        public async Task Should_Create_New_User()
        {
            // Arrange
            var data = UserMock.UserDTOFaker();
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
              (Models.User)null
            );
            environmentsRepository.Setup(x => x.FindByName(data.Environment))
              .ReturnsAsync(environment);
            passwordHasher.Setup(x => x.HashPassword(
              It.IsAny<Models.User>(), data.Password
            )).Returns(data.Password);
            usersRepository.Setup(x => x.Create(
              It.IsAny<Models.User>(), data.Environment
            )).ReturnsAsync(user);

            // Act
            var response = await usersService.CreateNewUser(data);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(user.Email, response.Email);
        }

        [Fact]
        public async Task Should_Not_Create_User_If_Email_Not_Unique() {
            // Arrange
            var data = UserMock.UserDTOFaker();
            var user = UserMock.UserFaker();

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
              user
            );

            // Act

            // Assert
            await Assert.ThrowsAsync<UserException>(
              () => usersService.CreateNewUser(data)
            );
        }

        [Fact]
        public async Task Should_Not_Create_User_If_Environment_Not_Exist() {
            // Arrange
            var data = UserMock.UserDTOFaker();
            var user = UserMock.UserFaker();

            usersRepository.Setup(x => x.FindByEmail(data.Email)).ReturnsAsync(
              (Models.User)null
            );
            environmentsRepository.Setup(x => x.FindByName(data.Environment))
              .ReturnsAsync((Models.Environment)null);

            // Act

            // Assert
            await Assert.ThrowsAsync<EnvironmentException>(
              () => usersService.CreateNewUser(data)
            );
        }
    }
}
