using Application.Actors.Common;
using Application.Common.Models;
using FluentValidation;
using MediatR;

namespace Application.Actors.Queries.GetActors;

public class GetActorsQuery : PaginatedRequest, IRequest<IPaginatedList<ActorResponse>>
{
    public string? SearchQuery { get; set; }
    public IList<Guid>? MovieIds { get; set; }
}

public class GetActorsQueryValidator : AbstractValidator<GetActorsQuery>
{
    public GetActorsQueryValidator()
    {
        // TODO find a way to automate this
        Include(new PaginatedRequestValidator());
        RuleFor(query => query.SearchQuery).MinimumLength(3).When(x => x.SearchQuery != null);
    }
}
