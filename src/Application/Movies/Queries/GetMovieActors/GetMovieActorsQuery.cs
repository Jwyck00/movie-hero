using Application.Actors.Common;
using MediatR;

namespace Application.Movies.Queries.GetMovieActors;

public class GetMovieActorsQuery : IRequest<IList<ActorResponse>>
{
    public Guid MovieId { get; set; }
}
