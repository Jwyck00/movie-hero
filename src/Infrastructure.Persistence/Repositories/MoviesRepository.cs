using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class MoviesRepository : Repository<Movie>, IMoviesRepository
{
    public MoviesRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
