using Application.Actors.Common;
using Application.Contracts.Movies;
using Application.Movies.Commands;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Common;
using Application.Movies.Queries;
using Application.Movies.Queries.GetMovie;
using Application.Movies.Queries.GetMovieActors;
using Application.Movies.Queries.GetMovies;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[Route("movies")]
public class MoviesController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public MoviesController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MovieResponse>> GetMovies([FromQuery] GetMoviesParams @params)
    {
        var command = new GetMoviesQuery
        {
            SearchQuery = @params.SearchQuery,
            PageNumber = @params.PageNumber,
            PageSize = @params.PageSize
        };

        var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MovieResponse>> GetMovie(Guid id)
    {
        var command = new GetMovieQuery(id);
        var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [AllowAnonymous]
    [HttpGet("{movieId:guid}/actors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ActorResponse>>> GetMovieActors(Guid movieId)
    {
        var command = new GetMovieActorsQuery { MovieId = movieId };
        var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MovieResponse>> CreateMovie(
        CreateMoviesRequest createMoviesRequest
    )
    {
        var command = _mapper.Map<CreateMovieCommand>(createMoviesRequest);
        var movie = await _mediator.Send(command);
        return Ok(movie);
    }
}
