using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.Persistence.EF.Models;
using ErrorCenter.Persistence.EF.Context;
using ErrorCenter.Persistence.EF.Repository;
using ErrorCenter.Services.Services;
using ErrorCenter.Persistence.EF.IRepository;
using ErrorCenter.Services.IServices;
using ErrorCenter.Services.Providers.StorageProvider.Model;
using ErrorCenter.Services.Providers.StorageProvider.Implementations;

namespace ErrorCenter.WebAPI.Configuration {
  public static class DependencyInjectionConfig {
    public static IServiceCollection ResolveDependencies(this IServiceCollection services) {
      services.AddScoped<ErrorCenterDbContext>();

      services.AddScoped<IErrorLogRepository<ErrorLog>, ErrorLogRepository>();
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<IStorageProvider, DiskStorageProvider>();

      services.AddTransient<IAuthenticateUserService, AuthenticateUserService>();
      services.AddTransient<IErrorLogService, ArchiveErrorLogService>();

      services.AddTransient<IUserAvatarUploadService, UserAvatarUploadService>();

      return services;
    }
  }
}
