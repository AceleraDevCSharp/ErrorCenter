using System;
using System.Threading.Tasks;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.WebAPI.Controllers;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.Tests.Tests {
  public class UsersControllerTest {
    private readonly Mock<IUsersService> usersService;
    private readonly Mock<IMapper> mapper;

    public UsersControllerTest() {
      usersService = new Mock<IUsersService>();
      mapper = new Mock<IMapper>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_When_User_Created() {
      // Arrange
      var newUser = new UserDTO() {
        Email = "johndoe@example.com",
        Password = "123456",
        Environment = "Development"
      };

      var user = new User() {
        Id = Guid.NewGuid().ToString(),
        Email = "johndoe@example.com",
        Avatar = "default.png",
        CreatedAt = DateTime.Now,
      };

      var createdUser = new UserViewModel() {
        Id = user.Id,
        Email = user.Email,
        Avatar = user.Avatar,
        CreatedAt = user.CreatedAt
      };

      usersService.Setup(x => x.CreateNewUser(newUser)).ReturnsAsync(user);
      mapper.Setup(x => x.Map<UserViewModel>(user)).Returns(createdUser);
      var usersController = new UsersController(
        usersService.Object,
        mapper.Object
      );

      // Act
      var response = await usersController.Create(newUser);

      // Assert
      Assert.IsType<CreatedResult>(response.Result);
    }

    [Theory]
    [InlineData("", "password", "SomeEnvironment")]
    [InlineData("invalid email", "password", "SomeEnvironment")]
    [InlineData(
      "A8BedcDwfjuAy3Pqm6xZ9yyTOTKc3xEoN8JQWng11JFX5ljymz76Bv32RZmLBdOHYnXPIxIY2kpVS7BbBZuoujePjnBUbBZhkMX1G",
      "password",
      "SomeEnvironment"
    )]
    [InlineData("johntre@example.com", "", "SomeEnvironment")]
    [InlineData("johntre@example.com", "12345", "SomeEnvironment")]
    [InlineData("johntre@example.com", "123456789abcd", "SomeEnvironment")]
    [InlineData("johntre@example.com", "123456", "")]
    public async Task Should_Throw_Exception_If_DTO_Invalid(
      string email,
      string password,
      string environment
    ) {
      // Arrange
      var newUser = new UserDTO() {
        Email = email,
        Password = password,
        Environment = environment
      };

      var usersController = new UsersController(
        usersService.Object,
        mapper.Object
      );

      // Act

      // Assert
      await Assert.ThrowsAsync<ViewModelException>(
        () => usersController.Create(newUser)
      );
    }
  }
}
