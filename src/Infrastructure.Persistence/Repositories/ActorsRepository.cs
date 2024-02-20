using Application.Common.Interfaces.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.Repositories.Common;

namespace Infrastructure.Persistence.Repositories;

public class ActorsRepository : Repository<Actor>, IActorsRepository
{
    public ActorsRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}
