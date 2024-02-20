using Application.Movies.Common;
using MediatR;

namespace Application.Movies.Queries.GetMovie;

public record GetMovieQuery(Guid Id) : IRequest<MovieResponse>;
