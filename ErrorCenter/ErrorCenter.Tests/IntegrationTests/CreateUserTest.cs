using System;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Xunit;
using Newtonsoft.Json;

using ErrorCenter.WebAPI;
using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;

namespace ErrorCenter.Tests.IntegrationTests {
  public class CreateUserTest : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public CreateUserTest(CustomWebApplicationFactory<Startup> factory) {
      _factory = factory;
    }

    private async Task<HttpResponseMessage> ExecuteRequest(
      HttpClient client,
      UserDTO data
    ) {
      return await client.PostAsync("/v1/users",
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
      var client = _factory.CreateClient();
      var userData = new UserDTO() {
        Email = "johndoe@example.com",
        Password = "123456",
        Environment = "Development"
      };

      // Act
      var response = await ExecuteRequest(client, userData);

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
    public async void Should_Not_Be_Able_To_Create_User_With_Invalid_Data(
      string email, string password, string role
    ) {
      // Arrange
      var client = _factory.CreateClient();
      var userData = new UserDTO() {
        Email = email,
        Password = password,
        Environment = role
      };

      // Act
      var response = await ExecuteRequest(client, userData);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}
