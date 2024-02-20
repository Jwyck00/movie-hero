using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities;

public class MovieStarRating : BaseEntity<Guid>
{
    public int Rate { get; set; }
    public Guid MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
