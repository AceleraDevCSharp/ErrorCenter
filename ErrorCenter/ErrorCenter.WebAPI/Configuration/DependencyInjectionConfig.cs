using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Services.Providers.HashProvider.Implementations;
using ErrorCenter.Services.Providers.HashProvider.Models;
using ErrorCenter.Services.Services;
using ErrorCenter.Persistence.EF.IRepository;
using ErrorCenter.Services.IServices;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ErrorCenterDbContext>();

            services.AddTransient<IErrorLogRepository<ErrorLog>, ErrorLogRepository>();

            services.AddSingleton<IHashProvider, BCryptHashProvider>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddTransient<IAuthenticateUserService, AuthenticateUserService>();
            services.AddTransient<IArchiveErrorLogService, ArchiveErrorLogService>();

            return services;
        }
    }
}
