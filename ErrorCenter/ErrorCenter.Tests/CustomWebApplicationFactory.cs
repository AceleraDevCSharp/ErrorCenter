using System;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Tests.IntegrationTests {
  public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
      builder.ConfigureServices(services => {
        var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
            typeof(DbContextOptions<ErrorCenterDbContext>)
        );

        services.Remove(descriptor);

        services.AddDbContext<ErrorCenterDbContext>(options => {
          options.UseInMemoryDatabase("InMemoryDbForTesting");
        });

        var sp = services.BuildServiceProvider();

        using (var scope = sp.CreateScope()) {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<ErrorCenterDbContext>();
          var logger = scopedServices
            .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

          db.Database.EnsureCreated();

          try {
            // seed?
          } catch (Exception ex) {
            logger.LogError(ex, "An error occurred seeding the " +
              "database with data. Error: {Message}", ex.Message);
          }
        }
      });
    }

    protected async Task AuthenticateAsync(HttpClient client) {
      var response = await GetJwtAsync(client);

      client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("bearer", response.Token);
    }

    protected async Task<SessionResponseDTO> GetJwtAsync(HttpClient client) {
      var response = await client.PostAsync("/v1/sessions",
        new StringContent(
          JsonConvert.SerializeObject(
            new SessionRequestDTO() {
              Email = "johntre@example.com",
              Password = "123456-Bb",
            }
          ), Encoding.UTF8
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue("application/json")
          }
        }
      );

      return JsonConvert.DeserializeObject<SessionResponseDTO>(
        await response.Content.ReadAsStringAsync()
      );
    }

    public async Task<UserViewModel> CreateTestUser(HttpClient client) {
      var response = await client.PostAsync("/v1/users",
        new StringContent(
          JsonConvert.SerializeObject(
            new UserDTO() {
              Email = "johntest@example.com",
              Password = "123456-Bb",
              Environment = "Development"
            }
          )
        ) {
          Headers = {
            ContentType = new MediaTypeHeaderValue("application/json")
          }
        }
      );

      var user = JsonConvert.DeserializeObject<UserViewModel>(
        await response.Content.ReadAsStringAsync()
      );

      return user;
    }
  }
}