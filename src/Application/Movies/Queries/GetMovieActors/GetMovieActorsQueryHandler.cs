using Application.Actors.Common;
using Application.Common;
using Application.Common.Interfaces.Persistence;
using Application.Error.Exceptions;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Movies.Queries.GetMovieActors;

public class GetMovieActorsQueryHandler : IRequestHandler<GetMovieActorsQuery, IList<ActorResponse>>
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMapper _mapper;
    private readonly IInclude<Movie> _include = new IncludeBuilder<Movie>(
        x => x.Include(e => e.Actors)
    );

    public GetMovieActorsQueryHandler(IMoviesRepository moviesRepository, IMapper mapper)
    {
        _moviesRepository = moviesRepository;
        _mapper = mapper;
    }

    public async Task<IList<ActorResponse>> Handle(
        GetMovieActorsQuery request,
        CancellationToken cancellationToken
    )
    {
        // TODO Duplicate code here (@GetMovieQueryHandler)
        var movie = await _moviesRepository.GetAsync(
            predicate: m => m.Id == request.MovieId,
            include: _include,
            noTracking: true,
            cancellationToken: cancellationToken
        );

        if (movie is null)
            throw new NotFoundException("Movies", request.MovieId);

        var actors = _mapper.Map<IList<ActorResponse>>(movie.Actors);

        return actors;
    }
}
