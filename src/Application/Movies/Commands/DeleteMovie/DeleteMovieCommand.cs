using MediatR;

namespace Application.Movies.Commands.DeleteMovie;

public class DeleteMovieCommand : IRequest
{
    public Guid MovieId { get; set; }
}
