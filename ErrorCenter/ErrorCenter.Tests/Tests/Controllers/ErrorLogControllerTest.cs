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

        [Fact]
        public async Task Should_Return_200_Status_Code_When_ErrorLog_Created()
        {
            // Arrange
            var controller = FakeIdentitySetup();
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();

            var newErrorLogDTO = new ErrorLogDTO()
            {
                Environment = "Development",
                Details = "detalhes",
                Level = "level",
                Origin = "origem",
                Title = "titulo"

            };

            var newErrorLog = new ErrorLog()
            {
                Environment = environment,
                Details = newErrorLogDTO.Details,
                Level = newErrorLogDTO.Level,
                Origin = newErrorLogDTO.Origin,
                Title = newErrorLogDTO.Title,
                User = user
            };

            var newErrorLogView = new ErrorLogViewModel()
            {
                Id = newErrorLog.Id,
                Environment = environment.Name,
                Details = newErrorLog.Details,
                Level = newErrorLog.Level,
                Origin = newErrorLog.Origin,
                Title = newErrorLog.Title,
                Email = user.Email,
                Quantity = newErrorLog.Quantity

            };

            errorLogService.Setup(x => x.CreateNewErrorLog(newErrorLogDTO, user.Email)).
                ReturnsAsync(newErrorLog);

            mapper.Setup(x => x.Map<ErrorLogViewModel>(newErrorLog)).Returns(newErrorLogView);
            // Act
            var response = await controller.Create(newErrorLogDTO);
            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Theory]
        [InlineData("", "detalhes", "level", "origem", "titulo")]
        [InlineData("Development", "", "level", "origem", "titulo")]
        [InlineData("Development", "detalhes", "", "origem", "titulo")]
        [InlineData("Development", "detalhes", "level", "", "titulo")]
        [InlineData("Development", "detalhes", "level", "origem", "")]
        public async Task Should_Throw_Exception_If_Error_Log_DTO_Invalid(
          string environmentName, string details, string level, string origin, string title)
        {
            // Arrange
            var controller = FakeIdentitySetup();
            var environment = EnvironmentMock.EnvironmentFaker();
            var user = UserMock.UserFaker();

            var newErrorLogDTO = new ErrorLogDTO()
            {
                Environment = environmentName,
                Details = details,
                Level = level,
                Origin = origin,
                Title = title

            };

            var newErrorLog = new ErrorLog()
            {
                Environment = environment,
                Details = newErrorLogDTO.Details,
                Level = newErrorLogDTO.Level,
                Origin = newErrorLogDTO.Origin,
                Title = newErrorLogDTO.Title,
                User = user
            };

            var newErrorLogView = new ErrorLogViewModel()
            {
                Id = newErrorLog.Id,
                Environment = environment.Name,
                Details = newErrorLog.Details,
                Level = newErrorLog.Level,
                Origin = newErrorLog.Origin,
                Title = newErrorLog.Title,
                Email = user.Email,
                Quantity = newErrorLog.Quantity

            };

            // Act
            errorLogService.Setup(x => x.CreateNewErrorLog(newErrorLogDTO, user.Email)).
                ReturnsAsync(newErrorLog);

            mapper.Setup(x => x.Map<ErrorLogViewModel>(newErrorLog)).Returns(newErrorLogView);
            // Assert
            await Assert.ThrowsAsync<ViewModelException>(
              () => controller.Create(newErrorLogDTO)
            );
        }

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

        [Fact]
        public async Task Should_Return_200_Status_Code_When_ErrorLog_Deleted()
        {
            // Arrange
            var controller = FakeIdentitySetup();

            var errorLog = ErrorLogMock.SingleErrorLogModelFaker();

            errorLogService.Setup(x => x.DeleteErrorLog(
                errorLog.Id,
                "johndoe@example.com",
                "SomeRole"
            )).ReturnsAsync(errorLog);
            mapper.Setup(x => x.Map<ErrorLogViewModel>(errorLog))
              .Returns(new ErrorLogViewModel());

            // Act
            var response = await controller.Delete(errorLog.Id);

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
        }
    }
}
