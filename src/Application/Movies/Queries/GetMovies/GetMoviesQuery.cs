using Application.Movies.Common;
using MediatR;

namespace Application.Movies.Queries.GetMovies;

public record GetMoviesQuery(Guid Id) : IRequest<MoviesResponse>;
