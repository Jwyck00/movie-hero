using Application.Common;
using Application.Common.Error.Exceptions;
using Application.Common.Interfaces.Persistence;
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
        var movie = await _moviesRepository
            .GetQuery(noTracking: true)
            .Where(m => m.Id == query.Id)
            .ProjectToType<MovieResponse>()
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (movie is null)
            throw new NotFoundException("Movie", query.Id);

        return movie;
    }
}
