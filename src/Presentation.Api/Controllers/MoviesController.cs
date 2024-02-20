using Application.Actors.Common;
using Application.Movies.Commands.AddActorToMovie;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Commands.DeleteMovie;
using Application.Movies.Commands.EditMovie;
using Application.Movies.Commands.RateMovie;
using Application.Movies.Common;
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

    [Authorize]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MovieResponse>> EditMovie(Guid id, EditMoviesRequest request)
    {
        var command = new EditMovieCommand { MovieId = id, Name = request.Name };
        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task DeleteMovie(Guid id)
    {
        var command = new DeleteMovieCommand { MovieId = id };
        await _mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}/actors")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<ActorResponse>>> GetMovieActors(Guid id)
    {
        var command = new GetMovieActorsQuery { MovieId = id };
        var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [Authorize]
    [HttpPost("{id:guid}/add-actor/{actorId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task AddActorToMovie(Guid id, Guid actorId)
    {
        var command = new AddActorToMovieCommand { MovieId = id, ActorId = actorId };
        await _mediator.Send(command);
    }

    /// <summary>
    /// Add Anonymous Rate to a movie
    /// </summary>
    /// <param name="id">Id of the Movie</param>
    [AllowAnonymous]
    [HttpPost("{id:guid}/rate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task RateMovie(Guid id, RateMoviesRequest request)
    {
        var command = new RateMovieCommand { MovieId = id, Rate = request.Rate };
        await _mediator.Send(command);
    }
}
