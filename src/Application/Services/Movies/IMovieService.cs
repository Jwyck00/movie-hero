using Application.Dto.Movies;

namespace Application.Services.Movies;

public interface IMovieService
{
    public MoviesResponse GetMovie(Guid id);
}