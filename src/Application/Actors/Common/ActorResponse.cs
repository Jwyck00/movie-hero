using Domain.Entities;
using Mapster;

namespace Application.Actors.Common;

public class ActorResponse : IMapFrom<Actor>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
