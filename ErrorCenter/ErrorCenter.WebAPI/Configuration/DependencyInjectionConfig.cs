using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Services.Providers.StorageProvider.Model;
using ErrorCenter.Services.Providers.StorageProvider.Implementations;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(
          this IServiceCollection services
        )
        {
            services.AddScoped<ErrorCenterDbContext>();

            services.AddScoped<IErrorLogRepository<ErrorLog>, ErrorLogRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IEnvironmentsRepository, EnvironmentsRepository>();
            services.AddScoped<IStorageProvider, DiskStorageProvider>();

            services.AddTransient<IAuthenticateUserService, AuthenticateUserService>();
            services.AddTransient<IErrorLogService, ArchiveErrorLogService>();
            services.AddTransient<IMailToUserService, MailToUserService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IDetailsErrorLogService, DetailsErrorLogService>();

            services.AddTransient<IUserAvatarUploadService, UserAvatarUploadService>();

            return services;
        }
    }
}
