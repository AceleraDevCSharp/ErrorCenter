using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

using ErrorCenter.WebAPI;

namespace ErrorCenter.Tests.IntegrationTests {
  public class DeleteErrorLogTest
    : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public DeleteErrorLogTest(CustomWebApplicationFactory<Startup> factory) {
      _factory = factory;
    }

    private async Task<HttpResponseMessage> ExecuteRequest(
      HttpClient client,
      int errorLogId
    ) {
      return await client.PatchAsync(
        "/v1/error-logs/delete/" + errorLogId,
        null
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Delete_Error_Log() {
      // Arrange
      var client = _factory.CreateClient();
      await _factory.CreateTestUser(client);
      await _factory.AuthenticateAsync(client);

      // Act
      var response = await ExecuteRequest(client, 1);

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Delete_Error_Log_If_Not_Authenticated() {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await ExecuteRequest(client, 1);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
  }
}