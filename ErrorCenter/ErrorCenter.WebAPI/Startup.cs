using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using AutoMapper;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.WebAPI.Configuration;

namespace ErrorCenter.WebAPI {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      services.AddDbContext<ErrorCenterDbContext>(options => {
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddCors();

      services.AddControllers();

      services.AddAutoMapper(typeof(Startup));

      services.AddSwaggerConfig();

      services.AddAuthenticationConfig(Configuration);

      services.addExceptionHandlerMiddleware();

      services.ResolveDependencies();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
      );

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseSwaggerConfig();

      app.UseExceptionMiddlwareHandler();

      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });
    }
  }
}
