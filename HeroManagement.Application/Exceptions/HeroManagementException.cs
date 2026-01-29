namespace HeroManagement.Application;

public abstract class AppException(string message) : Exception(message)
{
    public abstract int StatusCode { get; }
}
