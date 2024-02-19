using Application.Movies.Common;
using MediatR;

namespace Application.Movies.Queries;

public record GetMoviesQuery(Guid Id) : IRequest<MoviesResponse>;
