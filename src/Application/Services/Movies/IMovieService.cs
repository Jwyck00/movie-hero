using Application.Dto.Movies;

namespace Application.Services.Movies;

public interface IMovieService
{
    public Task<MoviesResponse> GetMovie(Guid id);
    public Task<MoviesResponse> CreateMovie(MoviesRequest moviesRequest);
}