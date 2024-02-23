using Application.Actors.Common;
using Application.Common.Interfaces.Persistence;
using Application.Common.Mapping;
using Application.Common.Models;
using Mapster;
using MediatR;

namespace Application.Actors.Queries.GetActors;

public class GetActorsQueryHandler : IRequestHandler<GetActorsQuery, IPaginatedList<ActorResponse>>
{
    private readonly IActorsRepository _actorsRepository;

    public GetActorsQueryHandler(IActorsRepository actorsRepository)
    {
        _actorsRepository = actorsRepository;
    }

    public async Task<IPaginatedList<ActorResponse>> Handle(
        GetActorsQuery request,
        CancellationToken cancellationToken
    )
    {
        var actorQuery = _actorsRepository.GetQuery();

        if (request.SearchQuery != null)
            actorQuery = actorQuery.Where(
                x => x.Name.ToLower().StartsWith(request.SearchQuery.ToLower())
            );
        // Check if this actor plays in one of the movies listed in filters
        if (request.MovieIds != null)
            actorQuery = actorQuery.Where(a => a.Movies.Any(m => request.MovieIds.Contains(m.Id)));

        var movies = await actorQuery.ProjectToType<ActorResponse>().PaginatedListAsync(request);
        return movies;
    }
}
