using Domain.Common.Interfaces;

namespace Domain.Entities;

public class Movie: IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
}