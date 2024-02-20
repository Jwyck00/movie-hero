using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Common;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Commands.RateMovie;

public class RateMovieCommandHandler : IRequestHandler<RateMovieCommand>
{
    private readonly IMoviesRepository _moviesRepository;

    public RateMovieCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task Handle(RateMovieCommand command, CancellationToken cancellationToken)
    {
        var movie = await _moviesRepository
            .GetQuery()
            .Where(m => m.Id == command.MovieId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", command.MovieId);

        movie.MovieStarRatings.Add(new MovieStarRating { Rate = command.Rate });

        await _moviesRepository.UpdateAsync(entity: movie, cancellationToken: cancellationToken);
    }
}
