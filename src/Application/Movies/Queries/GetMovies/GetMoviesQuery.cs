using Application.Models;
using Application.Movies.Common;
using FluentValidation;
using MediatR;

namespace Application.Movies.Queries.GetMovies;

public class GetMoviesQuery : PaginatedRequest, IRequest<IPaginatedList<MovieResponse>>
{
    public string? SearchQuery { get; set; }
}

public class GetMoviesQueryValidator : AbstractValidator<GetMoviesQuery>
{
    public GetMoviesQueryValidator()
    {
        // TODO find a way to automate this
        Include(new PaginatedRequestValidator());
        RuleFor(query => query.SearchQuery).MinimumLength(3).When(x => x.SearchQuery != null);
    }
}
