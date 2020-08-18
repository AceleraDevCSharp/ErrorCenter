using ErrorCenter.WebAPI;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace ErrorCenter.Tests.IntegrationTests
{
    public class MailServiceTest
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public MailServiceTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
          HttpClient client,
          string mail
        )
        {
            return await client.PostAsync(
              "v1/sessions/mail", new StringContent(mail)
            );
        }

        [Theory]
        [InlineData("asb@gmail.com")]
        [InlineData("as@gmail.com")]
        [InlineData("s@gmail.com")]
        public async void Should_Be_Able_To_Send_Mail_To_User(string mail)
        {
            //Arrange
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);

            //Act
            var response = await ExecuteRequest(client, mail);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("..41asb@gmail.com")]
        [InlineData("as@1541.com")]
        [InlineData("s@gmailcom")]
        public async void Should_Be_Able_To_Testing_Invalid_Mails(string mail)
        {
            //Arrange
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);

            //Act
            var response = await ExecuteRequest(client, mail);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }
    }
}
