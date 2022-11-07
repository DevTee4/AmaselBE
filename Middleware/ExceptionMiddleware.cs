using System.Net;
using BE.Services.Api.Configuration;
using Lib;
using Newtonsoft.Json;

namespace BE.Services.Api.Middleware
{
    public class ExceptionMiddleware
    {
         private Setting ConfigSetting ;
        private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next,Setting setting)
    {
         ConfigSetting= setting;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
          
            await HandleExceptionAsync(httpContext, ex,ConfigSetting);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception,Setting ConfigSetting)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var errorMsg = "";
        if(ConfigSetting.ApplicationMode!="Production"){
        errorMsg=  JsonConvert.SerializeObject( new Error()
        {   
            ErrorMsg=exception.Message+":" +exception?.InnerException?.Message,
            StatusCode = context.Response.StatusCode,
            FullLog = exception.StackTrace
         });
       
        }
        else{
       errorMsg = JsonConvert.SerializeObject(new Error()
        {   
            ErrorMsg="Server Error Please contact administrator.",
            StatusCode = context.Response.StatusCode,
            FullLog = "Server Error"
         });
        
        }
       return context.Response.WriteAsync(errorMsg);
    }

    }
     public static class CustomExceptionMiddleware
    {
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
      {       
       app.UseMiddleware<ExceptionMiddleware>();
      }
    }
}