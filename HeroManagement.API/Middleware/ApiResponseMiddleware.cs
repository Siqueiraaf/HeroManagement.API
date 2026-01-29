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
        var body = await new StreamReader(memoryStream).ReadToEndAsync();

        if (context.Response.StatusCode >= 400)
        {
            memoryStream.Seek(0, SeekOrigin.Begin);
            await memoryStream.CopyToAsync(originalBodyStream);
            return;
        }

        string message = context.Request.Method switch
        {
            "POST" => "Super-herói criado com sucesso!",
            "PUT" => "Informações do super-herói atualizadas com sucesso.",
            "DELETE" => "Super-herói excluído com sucesso.",
            _ => "Operação realizada com sucesso!"
        };

        object? data = null;
        if (!string.IsNullOrWhiteSpace(body))
        {
            try
            {
                data = JsonSerializer.Deserialize<object>(body);
            }
            catch { }
        }

        var wrappedResponse = new
        {
            success = true,
            data,
            message
        };

        var jsonResponse = JsonSerializer.Serialize(wrappedResponse);
        context.Response.ContentType = "application/json";
        context.Response.Body = originalBodyStream;

        context.Response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(jsonResponse);

        await context.Response.WriteAsync(jsonResponse);
    }
}
