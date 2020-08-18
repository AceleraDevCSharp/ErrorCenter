using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ErrorCenter.Tests.IntegrationTests
{
    public class DetailsErrorLogTest
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DetailsErrorLogTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
          HttpClient client,
          int id
        )
        {
            return await client.GetAsync(
              "v1/error-logs/error-details/" + id
            );
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Find_An_Error_That_Does_not_Exist()
        {
            // Arrange
            var id = 415415;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            // Act

            var response = await ExecuteRequest(client, id);

            // Assert

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void Should_Be_Able_To_Find_Error()
        {
            // Arrange
            var id = 1;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            // Act

            var response = await ExecuteRequest(client, id);

            // Assert

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void Should_Not_Be_Able_To_Find_An_Error_That_Does_not_Exist_If_Not_Authenticated()
        {
            // Arrange
            var id = 415415;
            var client = _factory.CreateClient();

            // Act

            var response = await ExecuteRequest(client, id);

            // Assert

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }



    }
}
