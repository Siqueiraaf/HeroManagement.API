using System.Text.Json;

namespace HeroManagement.API;

public class ApiResponseMiddleware
{
    private readonly RequestDelegate _next;

    public ApiResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        await _next(context);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var body = new StreamReader(memoryStream).ReadToEnd();
        memoryStream.Seek(0, SeekOrigin.Begin);

        if (context.Response.StatusCode < 400)
        {
            object? data = null;
            string message = "Operação realizada com sucesso!";

            if (!string.IsNullOrWhiteSpace(body))
            {
                data = JsonSerializer.Deserialize<object>(body);
            }

            if (string.IsNullOrWhiteSpace(body))
            {
                message = "Super-herói excluído com sucesso.";
            }

            else if (body.StartsWith("{") && body.Contains("Id"))
            {
                message = "Informações do super-herói atualizadas com sucesso.";
            }

            var wrappedResponse = new
            {
                success = true,
                data = data,
                message = message
            };

            context.Response.ContentType = "application/json";
            context.Response.Body = originalBodyStream;
            context.Response.StatusCode = context.Response.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(wrappedResponse));
        }
        else
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
        }
    }
}
