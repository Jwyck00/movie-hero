using Domain.Common;

namespace Domain.Entities;

public class Movie : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public IList<Actor> Actors { get; } = new List<Actor>();
    public IList<MovieStarRating> MovieStarRatings { get; } = new List<MovieStarRating>();
}
