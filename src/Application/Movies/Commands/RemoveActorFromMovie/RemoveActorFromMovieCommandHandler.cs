using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.RemoveActorFromMovie;

public class RemoveActorFromMovieCommandHandler : IRequestHandler<RemoveActorFromMovieCommand>
{
    private readonly IMoviesRepository _moviesRepository;

    public RemoveActorFromMovieCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task Handle(
        RemoveActorFromMovieCommand command,
        CancellationToken cancellationToken
    )
    {
        var movie = await _moviesRepository
            .GetQuery()
            .Include(x => x.Actors)
            .Where(m => m.Id == command.MovieId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", command.MovieId);

        var movieActor = movie.Actors.FirstOrDefault(x => x.Id == command.ActorId);

        if (movieActor is null)
            throw new NotFoundException("Actor Of Movie", command.ActorId);

        movie.Actors.Remove(movieActor);
        await _moviesRepository.UpdateAsync(entity: movie, cancellationToken: cancellationToken);
    }
}
