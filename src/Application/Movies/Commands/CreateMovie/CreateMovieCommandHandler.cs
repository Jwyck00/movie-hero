using Application.Common.Interfaces.Persistence;
using Application.Movies.Common;
using Domain.Entities;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MovieResponse>
{
    private readonly IMoviesRepository _moviesRepository;

    public CreateMovieCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task<MovieResponse> Handle(
        CreateMovieCommand command,
        CancellationToken cancellationToken
    )
    {
        var movieEntity = new Movie { Id = Guid.NewGuid(), Name = command.Name };

        await _moviesRepository.AddAsync(entity: movieEntity, cancellationToken: cancellationToken);
        return new MovieResponse { Id = movieEntity.Id, Name = movieEntity.Name };
    }
}
