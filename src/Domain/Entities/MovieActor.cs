using Domain.Common;

namespace Domain.Entities;

public class MovieActor : Entity
{
    public Guid MovieId { get; set; }
    public Guid ActorId { get; set; }

    public Movie Movie { get; } = null!;
    public Actor Actor { get; } = null!;
}
