using FluentValidation;

namespace Application.Models;

public class PaginatedRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}

public class PaginatedRequestValidator : AbstractValidator<PaginatedRequest>
{
    public PaginatedRequestValidator()
    {
        RuleFor(request => request.PageNumber).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(request => request.PageSize).NotNull().NotEmpty().GreaterThanOrEqualTo(5);
    }
}
