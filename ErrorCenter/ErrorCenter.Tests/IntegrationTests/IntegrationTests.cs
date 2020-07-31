using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ErrorCenter.WebAPI;
using ErrorCenter.Services.DTOs;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using Microsoft.Extensions.DependencyInjection.Extensions;

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

    protected async Task<SessionDTO> GetJwtAsync() {
      var response = await Client.PostAsync("/v1/sessions",
        new StringContent(
          JsonConvert.SerializeObject(
            new LoginInfoViewModel() {
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

      return JsonConvert.DeserializeObject<SessionDTO>(
        await response.Content.ReadAsStringAsync()
      );
    }

    protected async Task<UserViewModel> GetUser() {
      var response = await Client.PostAsync("/v1/users",
        new StringContent(
          JsonConvert.SerializeObject(
            new UserDTO() {
              Email = "johndoe@example.com",
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
