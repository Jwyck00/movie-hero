using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.RemoveActorFromMovie;

public class RemoveActorFromMovieCommand : IRequest
{
    public Guid MovieId { get; set; }
    public Guid ActorId { get; set; }
}
