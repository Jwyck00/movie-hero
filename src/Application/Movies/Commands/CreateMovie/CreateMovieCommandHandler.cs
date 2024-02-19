using Application.Common.Interfaces.Persistence;
using Application.Movies.Common;
using Domain.Entities;
using MediatR;

namespace Application.Movies.Commands.CreateMovie;

public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, MoviesResponse>
{
    private readonly IMovieRepository _movieRepository;

    public CreateMovieCommandHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MoviesResponse> Handle(
        CreateMovieCommand command,
        CancellationToken cancellationToken
    )
    {
        var movieEntity = new Movie { Id = Guid.NewGuid(), Name = command.Name };

        await _movieRepository.AddAsync(entity: movieEntity, cancellationToken: cancellationToken);
        return new MoviesResponse(Id: movieEntity.Id, Name: movieEntity.Name);
    }
}
