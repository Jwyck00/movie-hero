using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class MovieStartRatingsRepository : Repository<MovieStarRating>, IMovieStartRatingsRepository
{
    public MovieStartRatingsRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
