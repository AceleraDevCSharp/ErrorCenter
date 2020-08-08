using System.Net.Http;
using System.Threading.Tasks;

using Xunit;

using ErrorCenter.WebAPI;
using System.IO;
using System.Net;
using System;
using Newtonsoft.Json;

namespace ErrorCenter.Tests.IntegrationTests {
  public class UploadUserAvatarTest 
    : IClassFixture<CustomWebApplicationFactory<Startup>> {
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public UploadUserAvatarTest(CustomWebApplicationFactory<Startup> factory) {
      _factory = factory;
    }

    private async Task<HttpResponseMessage> ExecuteRequest(
      HttpClient client,
      bool uploadFile = true
    ) {
      if (uploadFile){
        var file = File.OpenRead(@"avatar-placeholder.png");
        var avatar = new StreamContent(file);
        var formData = new MultipartFormDataContent();
        formData.Add(avatar, "avatar", "avatar-placeholder.png");
        
        return await client.PatchAsync(
          "/v1/users/upload-avatar",
          formData
        );
      }

      return await client.PatchAsync(
        "/v1/users/upload-avatar",
        null
      );
    }

    [Fact]
    public async void Should_Be_Able_To_Upload_User_Avatar() {
      // Arrange
      var client = _factory.CreateClient();
      await _factory.CreateTestUser(client);
      await _factory.AuthenticateAsync(client);

      // Act
      var response = await ExecuteRequest(client);

      // Assert
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
      Assert.True(Guid.TryParse(
        Path.GetFileNameWithoutExtension(
          JsonConvert.DeserializeObject<string>(
            await response.Content.ReadAsStringAsync()
          )
        ),
        out Guid _
      ));
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Upload_Avatar_If_Not_Authenticated() {
      // Arrange
      var client = _factory.CreateClient();

      // Act
      var response = await ExecuteRequest(client);

      // Assert
      Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void Should_Not_Be_Able_To_Upload_If_No_Image_Was_Uploaded() {
      // Arrange
      var client = _factory.CreateClient();
      await _factory.AuthenticateAsync(client);

      // Act
      var response = await ExecuteRequest(client, false);

      // Assert
      Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
  }
}