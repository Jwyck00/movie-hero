using Application.Common.Interfaces.Persistence;
using Application.Dto.Movies;

namespace Application.Services.Movies;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    
    public MovieService(IMovieRepository movieRepository) {
        _movieRepository = movieRepository;
    }
    
    public MoviesResponse GetMovie(Guid id)
    {
        var movie =  _movieRepository.GetMovie(id);

        if (movie is null)
        {
            throw new Exception("Movie Not Found!");
        }
        
        return new MoviesResponse(Id: id, Name: movie.Name);
    }
}
