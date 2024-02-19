using Application.Common.Interfaces.Persistence;
using Application.Error.Exceptions;
using Application.Movies.Common;
using MapsterMapper;
using MediatR;

namespace Application.Movies.Queries;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, MoviesResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMoviesQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MoviesResponse> Handle(
        GetMoviesQuery query,
        CancellationToken cancellationToken
    )
    {
        var movie = await _movieRepository.GetAsync(
            predicate: m => m.Id == query.Id,
            noTracking: true,
            cancellationToken: cancellationToken
        );

        if (movie is null)
            throw new NotFoundException("Movie", query.Id);

        var resp = _mapper.Map<MoviesResponse>(movie);
        return resp;
    }
}
