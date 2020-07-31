using ErrorCenter.WebAPI;
using ErrorCenter.WebAPI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Xunit;

namespace ErrorCenter.Tests.IntegrationTests {
  public class CreateUser : IntegrationTests<Startup> {
    public CreateUser() : base() {}

    [Fact]
    public async void Should_Do_Something() {
      var user = await GetUser();
      Assert.Equal("johndoe@example.com", user.Email);
      /*
      var response = await Client.PostAsync("/v1/sessions", 
        new StringContent(
          JsonConvert.SerializeObject(
            new LoginInfoViewModel() {
              Email = "johndoe@example.com",
              Password = "123456-Bb",
            }
          ), Encoding.UTF8
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue("application/json")
          }
        }
      );

      Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
      */
    }
  }
}
