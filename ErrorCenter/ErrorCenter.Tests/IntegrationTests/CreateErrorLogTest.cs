using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Xunit;

using ErrorCenter.WebAPI;
using ErrorCenter.Services.DTOs;
using Newtonsoft.Json;
using System.Text;
using System;

namespace ErrorCenter.Tests.IntegrationTests
{
    public class CreateErrorLogTest
      : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateErrorLogTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
          HttpClient client, ErrorLogDTO errorLog
        )
        {
            return await client.PostAsync("/v1/error-logs/create",
              new StringContent(
                JsonConvert.SerializeObject(errorLog),
                Encoding.UTF8
              )
              {
                  Headers = {
            ContentType = new MediaTypeHeaderValue(
              "application/json"
            )
                }
              }
            );

        }

        [Fact]
        public async void Should_Be_Able_To_Create_Error_Log()
        {
            // Arrange
            // Arrange
            var errorLog = new ErrorLogDTO()
            {
                Environment = "Development",
                Level = "level1",
                Title = "titulo1",
                Details = "detalhes1",
                Origin = "origen1"
            };
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            // Act
            var response = await ExecuteRequest(client, errorLog);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("", "level", "titulo", "detalhes", "origem")]
        [InlineData("Development", "", "titulo", "detalhes", "origem")]
        [InlineData("Development", "level", "", "detalhes", "origem")]
        [InlineData("Development", "level", "titulo", "", "origem")]
        [InlineData("Development", "level", "titulo", "detalhes", "")]
        [InlineData("Dev", "level", "titulo", "detalhes", "origem")]
        [InlineData("Development", "le", "titulo", "detalhes", "origem")]
        [InlineData("Development", "level", "titu", "deta", "origem")]
        [InlineData("Development", "level", "titulo", "detalhes", "orig")]
        [InlineData("Development", "level",
            "Lorem ipsum eu litora donec egestas consectetur, integer tristique nibh aliquet magna, " +
            "sed euismod in quisque mollis.nostra dui interdum aliquam augue lectus consectetur vitae" +
            ", elementum metus maecenas sociosqu lacinia per amet scelerisque, lobortis in cursus " +
            "fringilla hendrerit sagittis.dictumst est ut egestas bibendum quis enim conubia magna " +
            "rutrum id, mollis conubia porttitor scelerisque nunc fermentum euismod ut dolor torquent " +
            "pharetra, dapibus scelerisque interdum malesuada enim elit fames tellus quam.", "detalhes", "orig")]
        [InlineData("Development", "level", "titulo",
            "Lorem ipsum quis iaculis fames auctor massa tempus pretium, luctus vulputate diam porttitor" +
            " curabitur auctor cras venenatis consequat, urna aliquam consequat sodales tincidunt dictum" +
            " praesent fames, sodales nunc auctor commodo aliquam nisi vivamus. mollis curabitur aenean" +
            " etiam habitasse suscipit laoreet volutpat amet per, malesuada pretium luctus malesuada" +
            " semper auctor faucibus mauris commodo placerat, tortor lacinia orci commodo maecenas" +
            " cubilia mauris mattis. curabitur molestie eget sodales magna varius, primis potenti a" +
            " aenean fringilla ipsum, lacus velit auctor orci. odio luctus vulputate cras dictumst" +
            " inceptos ullamcorper eros, molestie feugiat gravida sed purus imperdiet potenti justo," +
            " suspendisse lacinia fames semper vitae condimentum. Curabitur donec sociosqu viverra" +
            " id vel sit a gravida feugiat imperdiet, inceptos habitasse proin nulla quis fusce " +
            "accumsan donec. auctor posuere primis diam arcu viverra etiam ultrices curabitur tellus," +
            " auctor adipiscing lorem eu dapibus aptent habitant augue, auctor duis convallis dolor" +
            " donec vulputate ipsum id. ante risus fringilla orci in iaculis tellus leo augue habitasse," +
            " ut iaculis tempor felis consectetur vivamus potenti mattis, luctus eleifend tincidunt" +
            " maecenas aliquet viverra ad purus. accumsan egestas potenti gravida ad fusce justo " +
            "lacinia id, metus per urna dictum maecenas nisl senectus dapibus, viverra dolor facilisis" +
            " molestie curae hendrerit ullamcorper. Pharetra arcu aliquet condimentum nibh dapibus" +
            " litora egestas placerat turpis ", "origem")]
        public async void Should_Not_Be_Able_To_Create_Error_Log_With_Invalid_Data(string environment, string level,
            string title, string details, string origin
)
        {
            // Arrange
            var errorLog = new ErrorLogDTO()
            {
                Environment = environment,
                Level = level,
                Title = title,
                Details = details,
                Origin = origin
            };

            // Act
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            // Act
            var response = await ExecuteRequest(client, errorLog);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        //[Fact]
        //public async void Should_Not_Be_Able_To_Archive_Error_Log_If_Not_Authenticated()
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await ExecuteRequest(client, );

        //    // Assert
        //    Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        //}
    }
}