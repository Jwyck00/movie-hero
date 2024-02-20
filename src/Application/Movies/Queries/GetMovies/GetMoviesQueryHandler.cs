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
    private readonly IMovieRepository _movieRepository;

    public GetMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IPaginatedList<MovieResponse>> Handle(
        GetMoviesQuery request,
        CancellationToken cancellationToken
    )
    {
        var moviesQuery = _movieRepository.GetQuery();

        if (request.Q != null)
        {
            moviesQuery = moviesQuery.Where(x => x.Name.Contains(request.Q));
        }

        var movies = await moviesQuery.ProjectToType<MovieResponse>().PaginatedListAsync(request);
        return movies;
    }
}
