using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.AddActorToMovie;

public class AddActorToMovieCommandHandler : IRequestHandler<AddActorToMovieCommand>
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IActorsRepository _actorsRepository;

    public AddActorToMovieCommandHandler(
        IMoviesRepository moviesRepository,
        IActorsRepository actorsRepository
    )
    {
        _moviesRepository = moviesRepository;
        _actorsRepository = actorsRepository;
    }

    public async Task Handle(AddActorToMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", command.MovieId);

        var actor = await _actorsRepository
            .GetQuery()
            .Where(m => m.Id == command.ActorId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (actor is null)
            throw new NotFoundException("Actor", command.ActorId);

        movie.Actors.Add(actor);
        await _moviesRepository.UpdateAsync(entity: movie, cancellationToken: cancellationToken);
    }
}
