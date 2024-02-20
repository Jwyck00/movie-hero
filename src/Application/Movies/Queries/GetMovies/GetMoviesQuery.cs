using Application.Models;
using Application.Movies.Common;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Movies.Queries.GetMovies;

public record GetMoviesQuery(string? Q) : PaginatedRequest, IRequest<IPaginatedList<MovieResponse>>;

public class GetMoviesQueryValidator : AbstractValidator<GetMoviesQuery>
{
    public GetMoviesQueryValidator()
    {
        // TODO find a way to automate this
        Include(new PaginatedRequestValidator());
        RuleFor(query => query.Q).MinimumLength(3).When(x => x.Q != null);
    }
}
