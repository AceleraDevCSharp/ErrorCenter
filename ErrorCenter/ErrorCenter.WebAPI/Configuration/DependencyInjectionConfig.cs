using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Persistence.EF.Repositories;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ErrorCenterDbContext>();

            services.AddTransient<IErrorLogRepository<ErrorLog>, ErrorLogRepository>();

            return services;
        }
    }
}
