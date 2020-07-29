using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Services.Services;
using ErrorCenter.Services.IServices;
using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Services.Providers.StorageProvider.Model;
using ErrorCenter.Services.Providers.StorageProvider.Implementations;

namespace ErrorCenter.WebAPI.Configuration {
  public static class DependencyInjectionConfig {
    public static IServiceCollection ResolveDependencies(
      this IServiceCollection services
    ) {
      services.AddScoped<ErrorCenterDbContext>();

      services.AddScoped<IGetErrorLogService<ErrorLog>, GetErrorLogServoce>();
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<IStorageProvider, DiskStorageProvider>();

      services.AddTransient<IAuthenticateUserService, AuthenticateUserService>();
      services.AddTransient<IEditErrorLogService, EditErrorLogService>();
      services.AddTransient<IMailToUserService, MailToUserService>();

      services.AddTransient<IUserAvatarUploadService, UserAvatarUploadService>();

      return services;
    }
  }
}
