using System.Net;

namespace Application.Error.Exceptions;

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string? message)
        : base(statusCode: HttpStatusCode.Unauthorized, message: message) { }
}
