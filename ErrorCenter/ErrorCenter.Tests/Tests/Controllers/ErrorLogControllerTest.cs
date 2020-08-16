using System;
using System.Threading.Tasks;
using System.Security.Claims;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.DTOs;
using ErrorCenter.Services.Errors;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.WebAPI.Controllers;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Tests.UnitTests.Mocks;


namespace ErrorCenter.Tests.UnitTests.Controllers
{
    public class ErrorLogControllerTest
    {
        private readonly Mock<IErrorLogService> errorLogService;
        private readonly Mock<IErrorLogRepository<ErrorLog>> errorLogRepository;
        private readonly Mock<IDetailsErrorLogService> detailsErrorLogService;
        private readonly Mock<IMapper> mapper;

        public ErrorLogControllerTest()
        {
            errorLogService = new Mock<IErrorLogService>();
            errorLogRepository = new Mock<IErrorLogRepository<ErrorLog>>();
            detailsErrorLogService = new Mock<IDetailsErrorLogService>();
            mapper = new Mock<IMapper>();
        }

        private ErrorLogsController FakeIdentitySetup() {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim [] {
                        new Claim(ClaimTypes.Email, "johndoe@example.com"),
                        new Claim(ClaimTypes.Role, "SomeEnvironment")
                    }
                )
            );

            var controller = new ErrorLogsController(
                errorLogService.Object,
                errorLogRepository.Object,
                detailsErrorLogService.Object,
                mapper.Object
            );
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext {
              User = user
            };

            return controller;
        }

        // [Fact]
        // public async Task Should_Return_200_Status_Code_When_ErrorLog_Created()
        // {
        //    // Arrange
        //    var newUser = new ErrorLogDTO()
        //    {
        //        Environment = "Development",

        //    };

        //     var user = new User()
        //     {
        //         Id = Guid.NewGuid().ToString(),
        //         Email = "johndoe@example.com",
        //         Avatar = "default.png",
        //         CreatedAt = DateTime.Now,
        //     };

        //     var createdUser = new UserViewModel()
        //     {
        //         Id = user.Id,
        //         Email = user.Email,
        //         Avatar = user.Avatar,
        //         CreatedAt = user.CreatedAt
        //     };

        //     errorLogService.Setup(x => x.CreateNewUser(newUser)).ReturnsAsync(user);
        //     mapper.Setup(x => x.Map<UserViewModel>(user)).Returns(createdUser);
        //     var usersController = new UsersController(
        //       errorLogService.Object,
        //       mapper.Object
        //     );

        //    // Act
        //    var response = await usersController.Create(newUser);

        //    // Assert
        //   Assert.IsType<CreatedResult>(response.Result);
        // }

        // [Theory]
        // [InlineData("", "password", "SomeEnvironment")]
        // [InlineData("invalid email", "password", "SomeEnvironment")]
        // [InlineData(
        //   "A8BedcDwfjuAy3Pqm6xZ9yyTOTKc3xEoN8JQWng11JFX5ljymz76Bv32RZmLBdOHYnXPIxIY2kpVS7BbBZuoujePjnBUbBZhkMX1G",
        //   "password",
        //   "SomeEnvironment"
        // )]
        // [InlineData("johntre@example.com", "", "SomeEnvironment")]
        // [InlineData("johntre@example.com", "12345", "SomeEnvironment")]
        // [InlineData("johntre@example.com", "123456789abcd", "SomeEnvironment")]
        // [InlineData("johntre@example.com", "123456", "")]
        // public async Task Should_Throw_Exception_If_DTO_Invalid(
        //   string email,
        //   string password,
        //   string environment
        // )
        // {
        //     // Arrange
        //     var newUser = new UserDTO()
        //     {
        //         Email = email,
        //         Password = password,
        //         Environment = environment
        //     };

        //       var usersController = new UsersController(
        //         usersService.Object,
        //         mapper.Object
        //       );

        //     // Act

        //     // Assert
        //     await Assert.ThrowsAsync<ViewModelException>(
        //       () => usersController.Create(newUser)
        //     );
        // }

        [Fact]
        public async Task Should_Return_200_Status_Code_When_ErrorLog_Archived() {
          // Arrange
          var controller = FakeIdentitySetup();
          
          var errorLog = ErrorLogMock.SingleErrorLogModelFaker();
          errorLog.ArquivedAt = DateTime.Now;

          errorLogService.Setup(x => x.ArchiveErrorLog(
              errorLog.Id,
              "johndoe@example.com",
              "SomeRole"
          )).ReturnsAsync(errorLog);
          mapper.Setup(x => x.Map<ErrorLogViewModel>(errorLog))
            .Returns(new ErrorLogViewModel());

          // Act
          var response = await controller.Archive(errorLog.Id);

          // Assert
          Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
