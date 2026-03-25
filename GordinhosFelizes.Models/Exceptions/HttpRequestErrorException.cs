namespace GordinhosFelizes.Domain.Exceptions;

public class HttpRequestErrorException : Exception
{
    public HttpRequestErrorException(string message) : base(message) { }
}
