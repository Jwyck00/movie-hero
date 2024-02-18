using Application.Dto.Movies;
using Application.Services.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Api.Controllers;

[Route("movies")]
public class MoviesController : ApiControllerBase
{
    private readonly IMovieService _moviesService;

    public MoviesController(IMovieService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<MoviesResponse>> CreateMovie(MoviesRequest moviesRequest)
    {
        var movie = await _moviesService.CreateMovie(moviesRequest);
        return Accepted(movie);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status202Accepted)]
    public async Task<ActionResult<MoviesResponse>> GetMovies(Guid id)
    {
        var movie = await _moviesService.GetMovie(id);
        return Accepted(movie);
    }
}
