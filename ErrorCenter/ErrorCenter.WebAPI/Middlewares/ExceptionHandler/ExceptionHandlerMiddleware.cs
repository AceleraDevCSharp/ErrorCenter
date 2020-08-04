using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using ErrorCenter.Services.Errors;

namespace ErrorCenter.WebAPI.Middlewares.ExceptionHandler {
  public class ExceptionHandlerMiddleware : IMiddleware {
    private readonly IWebHostEnvironment Env;

    public ExceptionHandlerMiddleware(IWebHostEnvironment env) {
      Env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
      try {
        await next(context);
      } catch (ViewModelException vme) {
        await HandleViewModelExceptionAsync(context, vme);
      } catch (ErrorCenterException ac) {
        await HandleErrorCenterExceptionsAsync(context, ac);
      } catch (Exception ex) {
        await HandleUnknownExceptionAsync(context, ex);
      }
    }

    private Task HandleViewModelExceptionAsync(
      HttpContext context,
      ViewModelException exception
    ) {
      int statusCode = exception.StatusCode;

      var json = JsonConvert.SerializeObject(new {
        statusCode,
        message = exception.Message,
        details = exception.Details,
      }, new JsonSerializerSettings() {
        NullValueHandling = NullValueHandling.Ignore
      });

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }

    private Task HandleErrorCenterExceptionsAsync(
      HttpContext context,
      ErrorCenterException exception
    ) {
      int statusCode = exception.StatusCode;

      var json = JsonConvert.SerializeObject(new {
        statusCode,
        message = exception.Message,
      }, new JsonSerializerSettings() {
        NullValueHandling = NullValueHandling.Ignore
      });

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }

    private Task HandleUnknownExceptionAsync(
      HttpContext context,
      Exception exception
    ) {
      const int statusCode = StatusCodes.Status500InternalServerError;

      var json = JsonConvert.SerializeObject(new {
        statusCode,
        message = "An error occurred while processing your request",
        detailed = Env.IsDevelopment() ? exception : null,
      }, new JsonSerializerSettings() { 
        NullValueHandling = NullValueHandling.Ignore
      });

      context.Response.StatusCode = statusCode;
      context.Response.ContentType = "application/json";

      return context.Response.WriteAsync(json);
    }
  }
}
