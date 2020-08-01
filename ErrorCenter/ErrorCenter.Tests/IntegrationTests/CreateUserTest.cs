using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xunit;
using Newtonsoft.Json;

using ErrorCenter.WebAPI;
using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;

namespace ErrorCenter.Tests.IntegrationTests {
  public class CreateUserTest : IntegrationTests<Startup> {
    public CreateUserTest() : base() {}

    private async Task<HttpResponseMessage> ExecuteRequest(UserDTO data) {
      return await Client.PostAsync("/v1/users",
        new StringContent(
          JsonConvert.SerializeObject(data),
          Encoding.UTF8
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue(
              "application/json"
            )
          }
        }
      );
    } 

    [Fact]
    public async void Should_Be_Able_To_Create_A_User() {
      // Arrange
      var userData = new UserDTO() {
        Email = "johndoe@example.com",
        Password = "123456",
        Environment = "Development"
      };

      // Act
      var response = await ExecuteRequest(userData);

      var createdUser = JsonConvert
        .DeserializeObject<UserViewModel>(
          await response.Content.ReadAsStringAsync()
        );

      // Assert
      Assert.True(Guid.TryParse(createdUser.Id, out Guid _));
      Assert.Equal("johndoe@example.com", createdUser.Email);
      Assert.Equal(DateTime.Now.Date, createdUser.CreatedAt.Date);
    }

    [Theory]
    [InlineData("", "password", "some-role")]
    [InlineData("invalid email", "password", "some-role")]
    [InlineData(
      "A8BedcDwfjuAy3Pqm6xZ9yyTOTKc3xEoN8JQWng11JFX5ljymz76Bv32RZmLBdOHYnXPIxIY2kpVS7BbBZuoujePjnBUbBZhkMX1G",
      "password",
      "some-role"
    )]
    [InlineData("johntre@example.com", "", "some-role")]
    [InlineData("johntre@example.com", "12345", "some-role")]
    [InlineData("johntre@example.com", "123456789abcd", "some-role")]
    [InlineData("johntre@example.com", "123456", "")]
    public async void Should_Not_Be_Able_To_Create_User_With_Invalid_Data(
      string email, string password, string role
    ) {
      // Arrange
      var userData = new UserDTO() {
        Email = email,
        Password = password,
        Environment = role
      };

      // Act
      var response = await ExecuteRequest(userData);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Create_User_With_Not_Unique_Email() {
      // Arrange
      await CreateTestUser();

      var userData = new UserDTO() {
        Email = "johndoe@example.com",
        Password = "123456",
        Environment = "Development"
      };

      // Act
      var response = await ExecuteRequest(userData);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Create_User_With_Invalid_Environment() {
      var userData = new UserDTO() {
        Email = "johntre@example.com",
        Password = "123456",
        Environment = "invalid-environment"
      };

      // Act
      var response = await ExecuteRequest(userData);

      // Assert
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
  }
}
