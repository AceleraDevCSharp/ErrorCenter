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
using System;

namespace ErrorCenter.Tests.IntegrationTests {
  public class AuthenticationUserTest : IntegrationTests<Startup> {
    public AuthenticationUserTest() : base() {}

    private async Task<HttpResponseMessage> ExecuteRequest(SessionRequestDTO data) {
      return await Client.PostAsync("/v1/sessions",
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
    public async void Should_Be_To_Authentication_User() {
      // Arrange
      await CreateTestUser();

      var sessionData = new SessionRequestDTO() {
        Email = "johntest@example.com",
        Password = "123456-Bb"
      };

      // Act
      var response = await ExecuteRequest(sessionData);

      var createdSession = JsonConvert
        .DeserializeObject<SessionViewModel>(
          await response.Content.ReadAsStringAsync()
        );

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.Equal("johntest@example.com", createdSession.Email);
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
    public async void Should_Not_Be_Able_To_Login_With_Invalid_Data(
      string email, string password
    ) {
      // Arrange
      var data = new SessionRequestDTO() {
        Email = email,
        Password = password
      };

      // Act
      var response = await ExecuteRequest(data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Login_With_Non_Existing_User() {
      // Arrange
      var data = new SessionRequestDTO() {
        Email = "non.existing@example.com",
        Password = "password"
      };

      // Act
      var response = await ExecuteRequest(data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Login_With_Wrong_Password() {
      // Arrange
      await CreateTestUser();

      var data = new SessionRequestDTO() {
        Email = "johntest@example.com",
        Password = "wrongpass"
      };

      // Act
      var response = await ExecuteRequest(data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
  }
}