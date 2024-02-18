using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
