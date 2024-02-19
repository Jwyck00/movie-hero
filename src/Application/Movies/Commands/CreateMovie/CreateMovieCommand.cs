using Application.Movies.Common;
using MediatR;

namespace Application.Movies.Commands;

public record CreateMovieCommand(string Name) : IRequest<MoviesResponse>;
