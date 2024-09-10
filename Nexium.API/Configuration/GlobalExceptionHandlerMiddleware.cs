using System.Text.Json;
using Nexium.API.Exceptions;

namespace Nexium.API.Configuration;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        int status;
        string message;

        var exceptionType = exception.GetType();
        if (exceptionType == typeof(EntityReferencedException))
        {
            message = exception.Message;
            status = 400;
        }
        else if (exceptionType == typeof(EntityNotFoundException))
        {
            message = exception.Message;
            status = 404;
        }
        else if (exceptionType == typeof(OperationCanceledException))
        {
            status = 499;
            message = "The client canceled the request";
        }
        else
        {
            status = 500;
            message = exception.Message;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = status;
        return context.Response.WriteAsync(exceptionResult);
    }
}