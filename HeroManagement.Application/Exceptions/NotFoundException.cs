using Microsoft.AspNetCore.Http;

namespace HeroManagement.Application;

public class NotFoundException(string message) : AppException(message)
{
    public override int StatusCode => StatusCodes.Status404NotFound;
}
