using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using Application.Movies.Common;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.EditMovie;

public class EditMovieCommandHandler : IRequestHandler<EditMovieCommand, MovieResponse>
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IActorsRepository _actorsRepository;
    private readonly IMovieActorsRepository _movieActorsRepository;

    public EditMovieCommandHandler(
        IMoviesRepository moviesRepository,
        IActorsRepository actorsRepository,
        IMovieActorsRepository movieActorsRepository
    )
    {
        _moviesRepository = moviesRepository;
        _actorsRepository = actorsRepository;
        _movieActorsRepository = movieActorsRepository;
    }

    public async Task<MovieResponse> Handle(
        EditMovieCommand command,
        CancellationToken cancellationToken
    )
    {
        var movie = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", command.MovieId);

        var previousMovieActors = _movieActorsRepository
            .GetQuery()
            .Where(x => command.MovieId == x.MovieId);

        await _movieActorsRepository.DeleteRangeAsync(previousMovieActors, cancellationToken);

        var actors = await _actorsRepository
            .GetQuery()
            .Where(x => command.ActorIds.Contains(x.Id))
            .ToListAsync(cancellationToken: cancellationToken);

        movie.Name = command.Name;
        movie.Actors = actors;

        await _moviesRepository.UpdateAsync(entity: movie, cancellationToken: cancellationToken);

        var movieRes = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .ProjectToType<MovieResponse>()
            .FirstAsync(cancellationToken: cancellationToken);

        return movieRes;
    }
}
