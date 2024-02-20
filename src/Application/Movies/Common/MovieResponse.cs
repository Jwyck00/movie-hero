using Domain.Entities;
using Mapster;

namespace Application.Movies.Common;

public class MovieResponse : IMapFrom<Movie>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int ActorsCount { get; set; }
}

public class MovieResponseMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Movie, MovieResponse>()
            .Map(src => src.ActorsCount, des => des.Actors.Count);
    }
}
