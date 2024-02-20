using Domain.Entities;
using Mapster;

namespace Application.Movies.Common;

public class MovieResponse : IMapFrom<Movie>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public int ActorsCount { get; set; } = 0;

    public double RatingsAverage { get; set; } = 0.0;
    public int RatingsCount { get; set; } = 0;
}

public class MovieResponseMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config
            .NewConfig<Movie, MovieResponse>()
            .Map(src => src.RatingsCount, des => des.MovieStarRatings.Count)
            .Map(src => src.ActorsCount, des => des.Actors.Count)
            .Map(
                src => src.RatingsAverage,
                des => des.MovieStarRatings.Any() ? des.MovieStarRatings.Average(x => x.Rate) : 0.0
            );
    }
}
