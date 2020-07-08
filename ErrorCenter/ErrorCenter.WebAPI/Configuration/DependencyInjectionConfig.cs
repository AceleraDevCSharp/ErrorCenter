using ErrorCenter.Persistence.EF.Context;
using Microsoft.Extensions.DependencyInjection;
using ErrorCenter.Services.Interfaces;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.WebAPI.ViewModel;
using ErrorCenter.Persistence.EF.Repository.Model;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ErrorCenterDbContext>();

            services.AddScoped<IErrorLogRepository<ErrorLogModel>, ErrorLogRepository>();

            return services;
        }
    }
}
