using Domain.Entities;

namespace Application.Common.Interfaces.Persistence;

public interface IMovieRepository
{
    Movie? GetMovie(Guid id);
}