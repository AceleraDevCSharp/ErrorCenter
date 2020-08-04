using ErrorCenter.Services.DTOs;
using ErrorCenter.Tests.IntegrationTests;
using ErrorCenter.WebAPI;
using ErrorCenter.WebAPI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ErrorCenter.IntegrationTests.Tests {
  public class AuthenticateUserTest : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AuthenticateUserTest(CustomWebApplicationFactory<Startup> factory) {
      _factory = factory;
    }

    private async Task<HttpResponseMessage> ExecuteRequest(
      HttpClient client,
      SessionRequestDTO data
    ) {
      return await client.PostAsync("/v1/sessions",
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
      var client = _factory.CreateClient();
      await _factory.CreateTestUser(client);

      var sessionData = new SessionRequestDTO() {
        Email = "johntest@example.com",
        Password = "123456-Bb"
      };

      // Act
      var response = await ExecuteRequest(client, sessionData);

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
      var client = _factory.CreateClient();
      var data = new SessionRequestDTO() {
        Email = email,
        Password = password
      };

      // Act
      var response = await ExecuteRequest(client, data);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
  }
}
