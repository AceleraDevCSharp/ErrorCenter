using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Models;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ErrorCenterDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Environment>()
              .AddEntityFrameworkStores<ErrorCenterDbContext>()
              .AddDefaultTokenProviders();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            var key = Encoding.ASCII.GetBytes(configuration["JWTSecret"]);
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

      return services;
    }
  }
}
