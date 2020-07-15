using ErrorCenter.Persistence.EF.Context;
using Microsoft.Extensions.DependencyInjection;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Services.Interfaces;
using ErrorCenter.Services.Models;
using ErrorCenter.WebAPI.ViewModel;

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
