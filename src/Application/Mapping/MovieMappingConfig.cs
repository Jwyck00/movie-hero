using Application.Movies.Commands;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Common;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

public class MovieMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MoviesRequest, CreateMovieCommand>();
        config.NewConfig<Movie, MoviesResponse>();
    }
}
