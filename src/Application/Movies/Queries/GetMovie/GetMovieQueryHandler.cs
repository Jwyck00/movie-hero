using Application.Common.Interfaces.Persistence;
using Application.Error.Exceptions;
using Application.Movies.Common;
using MapsterMapper;
using MediatR;

namespace Application.Movies.Queries.GetMovie;

public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieResponse>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMapper _mapper;

    public GetMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _mapper = mapper;
    }

    public async Task<MovieResponse> Handle(
        GetMovieQuery query,
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

        var resp = _mapper.Map<MovieResponse>(movie);
        return resp;
    }
}
