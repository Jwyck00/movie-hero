using Application.Common.Interfaces.Persistence;
using Application.Dto.Movies;
using Application.Exceptions;
using Domain.Entities;

namespace Application.Services.Movies;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MoviesResponse> GetMovie(Guid id)
    {
        var movie = await _movieRepository.GetAsync(m => m.Id == id);
        if (movie is null)
            throw new NotFoundException("Movie", id);

        return new MoviesResponse(Id: id, Name: movie.Name);
    }

    public async Task<MoviesResponse> CreateMovie(MoviesRequest moviesRequest)
    {
        var movieEntity = new Movie { Id = Guid.NewGuid(), Name = moviesRequest.Name };
        await _movieRepository.AddAsync(movieEntity);

        return new MoviesResponse(Id: movieEntity.Id, Name: movieEntity.Name);
    }
}
