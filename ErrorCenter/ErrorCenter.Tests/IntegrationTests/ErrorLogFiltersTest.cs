using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.WebAPI;
using Microsoft.EntityFrameworkCore;
using Moq;
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
    public class ErrorLogFiltersTest
    : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public ErrorLogFiltersTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        private async Task<HttpResponseMessage> ExecuteRequest(
          HttpClient client, string route
        )
        {
            return await client.GetAsync("v1/error-logs/"+ route);
        }

        [Fact]
        public async void Should_be_Able_Return_All_Error_Log()
        {
            //Arrange
            var route = "";
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Should_be_Able_Return_All_Enviroments()
        {
            //Arrange
            var route = "environments";
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Should_be_Able_Return_All_Error_Log_Arquived()
        {
            //Arrange
            var route = "archived";
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Should_be_Able_Return_All_Error_Log_Deleted()
        {
            //Arrange
            var route = "deleted";
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Development")]
        [InlineData("Homologation")]
        [InlineData("Production")]
        [InlineData("a")]
        public async void Should_be_Able_Return_All_Error_Log_By_Enviroment(string env)
        {
            //Arrange
            var route = "environment=" + env;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level")]
        [InlineData("Development", "Frequencia")]
        [InlineData("Homologation", "Level")]
        [InlineData("Homologation", "Frequencia")]
        [InlineData("Production", "Level")]
        [InlineData("Production", "Frequencia")]

        [InlineData("a", "Level")]
        [InlineData("a", "Frequencia")]
        [InlineData("Production", "a")]
        public async void Should_be_Able_Return_All_Error_Log_By_Enviroment_And_Ordered_By(string env, string order)
        {
            //Arrange
            var route = "environment=" + env + "/" + "orderby=" + order;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level", "Level", "a")]
        [InlineData("Development", "Level", "Descricao", "a")]
        [InlineData("Development", "Level", "Origem", "a")]
        [InlineData("a", "Level", "Origem", "a")]
        [InlineData("Development", "Level", "a", "a")]
        [InlineData("Development", "a", "Level", "a")]
        [InlineData("Development", "a", "Descricao", "a")]
        [InlineData("Development", "a", "Origem", "a")]
        [InlineData("Development", "Frequencia", "Level", "a")]
        [InlineData("Development", "Frequencia", "Descricao", "a")]
        [InlineData("Development", "Frequencia", "Origem", "a")]
        [InlineData("Homologation", "Level", "Level", "a")]
        [InlineData("Homologation", "Level", "Descricao", "a")]
        [InlineData("Homologation", "Level", "Origem", "a")]
        [InlineData("Homologation", "Frequencia", "Level", "a")]
        [InlineData("Homologation", "Frequencia", "Descricao", "a")]
        [InlineData("Homologation", "Frequencia", "Origem", "a")]
        [InlineData("Production", "Level", "Level", "a")]
        [InlineData("Production", "Level", "Descricao", "a")]
        [InlineData("Production", "Level", "Origem", "a")]
        [InlineData("Production", "Frequencia", "Level", "a")]
        [InlineData("Production", "Frequencia", "Descricao", "a")]
        [InlineData("Production", "Frequencia", "Origem", "a")]
        public async void Should_be_Able_Return_All_Error_Log_By_Enviroment_And_Ordered_By_Search_By(string env, string order, string search, string textSearch)
        {
            //Arrange
            var route = "environment=" + env + "/orderby=" + order + "/typeSearch=" + search + "?textSearch=" + textSearch;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Development", "Level", "a")]
        [InlineData("Development", "Descricao", "a")]
        [InlineData("Development", "Origem", "a")]
        [InlineData("Homologation", "Level", "a")]
        [InlineData("Homologation", "Descricao", "a")]
        [InlineData("Homologation", "Origem", "a")]
        [InlineData("Production", "Level", "a")]
        [InlineData("Production", "Descricao", "a")]
        [InlineData("Production", "Origem", "a")]
        [InlineData("erro", "Origem", "a")]
        [InlineData("Production", "erro", "a")]
        public async void Should_be_Able_Return_All_Error_Log_By_Enviroment_Search_By(string env, string search, string textSearch)
        {
            //Arrange
            var route = "environment=" + env + "/typeSearch=" + search + "?textSearch=" + textSearch;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Level")]
        [InlineData("Frequencia")]
        [InlineData("Frequen")]
        public async void Should_be_Able_Return_All_Error_Log_Ordered_By(string order)
        {
            //Arrange
            var route = "orderby=" + order;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Level", "Level", "a")]
        [InlineData("Level", "Descricao", "a")]
        [InlineData("Level", "Origem", "a")]
        [InlineData("Frequencia", "Level", "a")]
        [InlineData("Frequencia", "Descricao", "a")]
        [InlineData("Frequencia", "Origem", "a")]
        [InlineData("a", "Origem", "a")]
        [InlineData("Frequencia", "a", "a")]
        [InlineData("a", "a", "a")]
        public async void Should_be_Able_Return_All_Error_Log_Ordered_By_Search_By(string order, string search, string textSearch)
        {
            //Arrange
            var route = "orderby=" + order + "/typeSearch=" + search + "?textSearch=" + textSearch;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("Level", "a")]
        [InlineData("Descricao", "a")]
        [InlineData("Origem", "a")]
        [InlineData("a", "a")]
        public async void Should_be_Able_Return_All_Error_Log_Search_By(string search, string textSearch)
        {
            //Arrange
            var route = "typeSearch=" + search + "?textSearch=" + textSearch;
            var client = _factory.CreateClient();
            await _factory.CreateTestUser(client);
            await _factory.AuthenticateAsync(client);

            //Act
            var response = await ExecuteRequest(client, route);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
