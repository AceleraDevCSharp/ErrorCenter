using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.WebAPI;
using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Context;

namespace ErrorCenter.Tests.IntegrationTests {
  public class IntegrationTests<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {
    public HttpClient Client { get; private set; }

    public IntegrationTests() {
      var server = new WebApplicationFactory<Startup>()
        .WithWebHostBuilder(builder => {
          builder.ConfigureServices(services => {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<ErrorCenterDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<ErrorCenterDbContext>(options => {
              options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope()) {
              var scopedServices = scope.ServiceProvider;
              var db = scopedServices.GetRequiredService<ErrorCenterDbContext>();

              db.Database.EnsureCreated();
            }
          });
        });

      Client = server.CreateClient();
    }

    protected async Task AuthenticateAsync() {
      var response = await GetJwtAsync();

      Client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("bearer", response.Token);
    }

    protected async Task<SessionResponseDTO> GetJwtAsync() {
      var response = await Client.PostAsync("/v1/sessions",
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

    protected async Task<UserViewModel> CreateTestUser() {
      var response = await Client.PostAsync("/v1/users",
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
