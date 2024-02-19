using System.Net;
using FluentValidation.Results;

namespace Application.Error.Exceptions;

public class ArgValidationException : ApiException
{
    public ArgValidationException(IEnumerable<ValidationFailure> errors)
        : base(statusCode: HttpStatusCode.NotFound, message: BuildErrorMessage(errors)) { }

    private static string BuildErrorMessage(IEnumerable<ValidationFailure> errors)
    {
        var arr = errors.Select(
            x => $"{Environment.NewLine} -- {x.PropertyName}: {x.ErrorMessage}"
        );
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}
