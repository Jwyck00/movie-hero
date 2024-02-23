using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class MovieActorsRepository : Repository<MovieActor>, IMovieActorsRepository
{
    public MovieActorsRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
