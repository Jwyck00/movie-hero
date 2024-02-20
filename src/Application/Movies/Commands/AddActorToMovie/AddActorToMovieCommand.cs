using FluentValidation;
using MediatR;

namespace Application.Movies.Commands.AddActorToMovie;

public class AddActorToMovieCommand : IRequest
{
    public Guid MovieId { get; set; }
    public Guid ActorId { get; set; }
}
