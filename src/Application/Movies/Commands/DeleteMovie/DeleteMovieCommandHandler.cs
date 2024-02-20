using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMovieStartRatingsRepository _startRatingsRepository;

    public DeleteMovieCommandHandler(
        IMoviesRepository moviesRepository,
        IMovieStartRatingsRepository startRatingsRepository
    )
    {
        _moviesRepository = moviesRepository;
        _startRatingsRepository = startRatingsRepository;
    }

    public async Task Handle(DeleteMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", command.MovieId);

        var movieStarRatings = _startRatingsRepository
            .GetQuery()
            .Where(m => m.MovieId == command.MovieId);

        await _startRatingsRepository.DeleteRangeAsync(
            entities: movieStarRatings,
            cancellationToken: cancellationToken
        );
        
        await _moviesRepository.DeleteAsync(entity: movie, cancellationToken: cancellationToken);
    }
}
