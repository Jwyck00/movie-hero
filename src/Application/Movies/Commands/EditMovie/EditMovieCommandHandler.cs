using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using Application.Movies.Common;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.EditMovie;

public class EditMovieCommandHandler : IRequestHandler<EditMovieCommand, MovieResponse>
{
    private readonly IMoviesRepository _moviesRepository;

    public EditMovieCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
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

        movie.Name = command.Name;

        await _moviesRepository.UpdateAsync(entity: movie, cancellationToken: cancellationToken);

        var movieRes = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .ProjectToType<MovieResponse>()
            .FirstAsync(cancellationToken: cancellationToken);

        return movieRes;
    }
}
