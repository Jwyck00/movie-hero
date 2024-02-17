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

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MoviesResponse), StatusCodes.Status202Accepted)]
    public ActionResult<MoviesResponse> GetMovies(Guid id)
    {
        var movie = _moviesService.GetMovie(id);
        return Accepted(movie);
    }
}