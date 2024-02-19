using Application.Movies.Commands;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Common;
using Application.Movies.Queries;
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

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MoviesResponse>> CreateMovie(MoviesRequest moviesRequest)
    {
        var command = _mapper.Map<CreateMovieCommand>(moviesRequest);
        var movie = await _mediator.Send(command);
        return Ok(movie);
    }

    [AllowAnonymous]
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MoviesResponse>> GetMovies(Guid id)
    {
        var movie = await _mediator.Send(new GetMoviesQuery(id));
        return Ok(movie);
    }
}
