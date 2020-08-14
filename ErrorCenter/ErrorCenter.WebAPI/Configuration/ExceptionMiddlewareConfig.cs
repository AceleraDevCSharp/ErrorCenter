using Microsoft.Extensions.DependencyInjection;

using ErrorCenter.WebAPI.Middlewares.ExceptionHandler;
using Microsoft.AspNetCore.Builder;

namespace ErrorCenter.WebAPI.Configuration
{
    public static class ExceptionMiddlewareConfig
    {
        public static IServiceCollection addExceptionHandlerMiddleware(this IServiceCollection services)
        {
            return services.AddTransient<ExceptionHandlerMiddleware>();
        }

        public static void UseExceptionMiddlwareHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
