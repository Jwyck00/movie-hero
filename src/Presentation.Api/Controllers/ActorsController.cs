using Application.Actors.Common;
using Application.Actors.Queries.GetActors;
using Application.Common.Models;
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

[Route("actors")]
public class ActorsController : ApiControllerBase
{
    private readonly ISender _mediator;

    public ActorsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IPaginatedList<ActorResponse>>> GetActors(
        [FromQuery] GetActorsParams @params
    )
    {
        var command = new GetActorsQuery
        {
            SearchQuery = @params.SearchQuery,
            MovieIds = @params.MovieIds,
            PageNumber = @params.PageNumber,
            PageSize = @params.PageSize
        };

        var movie = await _mediator.Send(command);
        return Ok(movie);
    }
}
