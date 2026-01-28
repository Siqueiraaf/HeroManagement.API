using HeroManagement.Application;
using System.Text.Json;

namespace HeroManagement.API;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await HandleAppException(context, ex);
        }
        catch (Exception)
        {
            await HandleUnknownException(context);
        }
    }

    private static async Task HandleAppException(HttpContext context, AppException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;

        var response = new
        {
            status = ex.StatusCode,
            error = ex.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static async Task HandleUnknownException(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            status = 500,
            error = "Erro interno do servidor"
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
