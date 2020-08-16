using System.IO;
using System.Threading.Tasks;
using System.Security.Claims;

using Moq;
using Xunit;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.Controllers;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Errors;

namespace ErrorCenter.Tests.UnitTests.Controllers {
  public class UserAvatarControllerTest {
    private readonly Mock<IUserAvatarUploadService> userAvatarUpload;

    public UserAvatarControllerTest() {
      userAvatarUpload = new Mock<IUserAvatarUploadService>();
    }

    [Fact]
    public async Task Should_Return_200_Status_Code_When_User_Uploads_Avatar() {
      // Arrange
      FormFile avatar;
      using (var stream = File.OpenRead(@"avatar-placeholder.png")) {
        avatar = new FormFile(
          stream,
          0,
          stream.Length,
          null,
          Path.GetFileName(stream.Name)
        );
      }

      var file = new UserAvatarDTO() {
        avatar = avatar
      };

      userAvatarUpload.Setup(x => x.UploadUserAvatar(
        "johndoe@example.com",
        file
      )).ReturnsAsync("Some/UserAvatar/URL.png");

      var user = new ClaimsPrincipal(
        new ClaimsIdentity(
          new Claim[] {
            new Claim(ClaimTypes.Email, "johndoe@example.com"),
            new Claim(ClaimTypes.Role, "Development")
          }
        )
      );

      var userAvatarController = new UserAvatarController(
        userAvatarUpload.Object
      );
      
      userAvatarController.ControllerContext = new ControllerContext();
      userAvatarController.ControllerContext.HttpContext = 
        new DefaultHttpContext {
          User = user
        };

      // Act
      var response = await userAvatarController.Update(file);

      // Assert
      Assert.IsType<OkObjectResult>(response.Result);
    }

    [Fact]
    public async Task Should_Throw_Exception_If_DTO_Invalid() {
      var file = new UserAvatarDTO() {};

      var user = new ClaimsPrincipal(
        new ClaimsIdentity(
          new Claim[] {
            new Claim(ClaimTypes.Email, "johndoe@example.com"),
            new Claim(ClaimTypes.Role, "Development")
          }
        )
      );

      var userAvatarController = new UserAvatarController(
        userAvatarUpload.Object
      );
      
      userAvatarController.ControllerContext = new ControllerContext();
      userAvatarController.ControllerContext.HttpContext = 
        new DefaultHttpContext {
          User = user
        };

      // Act

      // Assert
      await Assert.ThrowsAsync<FileUploadException>(
        () => userAvatarController.Update(file)
      );
    }
  }
}