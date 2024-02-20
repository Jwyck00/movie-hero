using Application.Common;
using Application.Common.Interfaces.Persistence;
using Application.Error.Exceptions;
using Application.Movies.Common;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Queries.GetMovie;

public class GetMovieQueryHandler : IRequestHandler<GetMovieQuery, MovieResponse>
{
    private readonly IMoviesRepository _moviesRepository;

    private readonly IMapper _mapper;

    public GetMovieQueryHandler(IMoviesRepository moviesRepository, IMapper mapper)
    {
        _moviesRepository = moviesRepository;
        _mapper = mapper;
    }

    public async Task<MovieResponse> Handle(
        GetMovieQuery query,
        CancellationToken cancellationToken
    )
    {
        var movieActor = await _moviesRepository
            .GetQuery(noTracking: true)
            .Where(m => m.Id == query.Id)
            .Select(movie => new { Movie = movie, ActorsCount = movie.Actors.Count })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movieActor is null)
            throw new NotFoundException("Movie", query.Id);

        var movie = _mapper.Map<MovieResponse>(movieActor.Movie);
        movie.ActorsCount = movieActor.ActorsCount;

        return movie;
    }
}
