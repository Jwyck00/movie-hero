using Domain.Common;

namespace Domain.Entities;

public class Movie : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public IList<Actor> Actors { get; set; } = new List<Actor>();
    public IList<MovieStarRating> MovieStarRatings { get; set; } = new List<MovieStarRating>();
}
