using Application.Movies.Commands;
using Application.Movies.Common;
using Application.Movies.Queries;
using MapsterMapper;
using MediatR;
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

    [HttpPost]
    [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<MoviesResponse>> CreateMovie(MoviesRequest moviesRequest)
    {
        var command = _mapper.Map<CreateMovieCommand>(moviesRequest);
        var movie = await _mediator.Send(command);
        return Accepted(movie);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<MoviesResponse>> GetMovies(Guid id)
    {
        var movie = await _mediator.Send(new GetMoviesQuery(id));
        return Accepted(movie);
    }
}
