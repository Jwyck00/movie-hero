using FluentValidation;

namespace Application.Models;

public record PaginatedRequest(int PageNumber = 1, int PageSize = 10);

public class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    public PaginatedRequestValidator()
    {
        RuleFor(request => request.PageNumber).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(request => request.PageSize).NotNull().NotEmpty().GreaterThanOrEqualTo(5);
    }
}
