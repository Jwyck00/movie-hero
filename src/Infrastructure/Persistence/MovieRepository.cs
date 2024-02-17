using Application.Common.Interfaces.Persistence;
using Domain.Entities;

namespace Infrastructure.Persistence;

public class MovieRepository : IMovieRepository
{
    private static IList<Movie> _movies = new List<Movie>(
        new[]
        {
            new Movie
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Name = "Only Movie We Have"
            }
        }
    );

    public Movie? GetMovie(Guid id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        return movie;
    }
}
