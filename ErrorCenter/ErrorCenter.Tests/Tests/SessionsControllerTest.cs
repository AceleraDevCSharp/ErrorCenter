using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.WebAPI.Controllers;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Errors;

namespace ErrorCenter.Tests.Tests {
  public class SessionsControllerTest {
    private readonly Mock<IAuthenticateUserService> authenticateUser;
    private readonly Mock<IMailToUserService> mailToUser;
    private readonly Mock<IMapper> mapper;

    public SessionsControllerTest() {
      authenticateUser = new Mock<IAuthenticateUserService>();
      mailToUser = new Mock<IMailToUserService>();
      mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_When_Authenticated() {
      // Arrange
      var login = new SessionRequestDTO() {
        Email = "johndoe@example.com",
        Password = "123456"
      };

      var sessionData = new SessionResponseDTO() {
        Email = "johndoe@example.com",
        Token = "JWTToken"
      };

      var session = new SessionViewModel() {
        Email = "johndoe@example.com",
        Token = "JWTToken"
      };

      authenticateUser.Setup(x => x.Authenticate(login)).ReturnsAsync(
        sessionData
      );

      mapper.Setup(x => x.Map<SessionViewModel>(sessionData)).Returns(
        session
      );

      var sessionsController = new SessionsController(
        authenticateUser.Object,
        mailToUser.Object,
        mapper.Object
      );

      // Act
      var response = await sessionsController.Create(login);

      // Assert
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Theory]
    [InlineData("", "password")]
    [InlineData("invalid email", "password")]
    [InlineData(
      "A8BedcDwfjuAy3Pqm6xZ9yyTOTKc3xEoN8JQWng11JFX5ljymz76Bv32RZmLBdOHYnXPIxIY2kpVS7BbBZuoujePjnBUbBZhkMX1G",
      "password"
    )]
    [InlineData("johntest@example.com", "")]
    [InlineData("johntest@example.com", "12345")]
    [InlineData("johntest@example.com", "123456789abcd")]
    public async Task Should_Throw_Exception_If_DTO_Invalid(
      string email,
      string password
    ) {
      // Arrange
      var login = new SessionRequestDTO() {
        Email = email,
        Password = password
      };

      var sessionsController = new SessionsController(
        authenticateUser.Object,
        mailToUser.Object,
        mapper.Object
      );

      // Act

      // Assert
      await Assert.ThrowsAsync<ViewModelException>(
        () => sessionsController.Create(login)
      );
    }
  }
}
