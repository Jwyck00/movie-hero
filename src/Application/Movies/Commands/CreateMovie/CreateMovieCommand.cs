using Application.Movies.Common;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public record CreateMovieCommand(string Name) : IRequest<MoviesResponse>;
