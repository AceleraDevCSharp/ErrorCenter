using Xunit;

using ErrorCenter.WebAPI;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using System;

namespace ErrorCenter.Tests.IntegrationTests {
  public class ArchiveErrorLogTest 
    : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public ArchiveErrorLogTest(CustomWebApplicationFactory<Startup> factory) {
      _factory = factory;
    }

    private async Task<HttpResponseMessage> ExecuteRequest(
      HttpClient client,
      int errorLogId
    ) {
      return await client.PatchAsync(
        "/v1/error-logs/archive/" + errorLogId,
        null
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Archive_Error_Log() {
      // Arrange
      var client = _factory.CreateClient();
      var user = await _factory.CreateTestUser(client);
      await _factory.AuthenticateAsync(client);

      // Act
      var response = await ExecuteRequest(client, 1);

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Archive_Error_Log_If_Not_Authenticated() {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await ExecuteRequest(client, 1);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
  }
}