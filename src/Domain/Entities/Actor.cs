using Domain.Common;

namespace Domain.Entities;

public class Actor : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;

    public IList<Movie> Movies { get; } = new List<Movie>();
}
