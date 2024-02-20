using Application.Common.Interfaces.Persistence;
using Application.Mapping;
using Application.Models;
using Application.Movies.Common;
using Mapster;
using MapsterMapper;
using MediatR;

namespace Application.Movies.Queries.GetMovies;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IPaginatedList<MovieResponse>>
{
    private readonly IMoviesRepository _moviesRepository;

    public GetMoviesQueryHandler(IMoviesRepository moviesRepository, IMapper mapper)
    {
        _moviesRepository = moviesRepository;
    }

    public async Task<IPaginatedList<MovieResponse>> Handle(
        GetMoviesQuery request,
        CancellationToken cancellationToken
    )
    {
        var moviesQuery = _moviesRepository.GetQuery();

        if (request.SearchQuery != null)
        {
            moviesQuery = moviesQuery.Where(x => x.Name.Contains(request.SearchQuery));
        }

        var movies = await moviesQuery.ProjectToType<MovieResponse>().PaginatedListAsync(request);

        return movies;
    }
}
