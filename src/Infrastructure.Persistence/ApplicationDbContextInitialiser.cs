using Application.Common.Interfaces.Persistence;
using Bogus;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser =
            scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly IMoviesRepository _movieRepository;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        IMoviesRepository movieRepository
    )
    {
        _logger = logger;
        _context = context;
        _movieRepository = movieRepository;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        var hasAnyMovie = await _movieRepository.Query.AnyAsync();

        if (!hasAnyMovie)
        {
            var actorFaker = new Faker<Actor>()
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Id, f => f.Random.Guid());

            var actors = actorFaker.Generate(15).ToList();

            var ratingFaker = new Faker<MovieStarRating>()
                .RuleFor(u => u.Rate, f => f.Random.Int(1, 5))
                .RuleFor(u => u.Id, f => f.Random.Guid());

            var movieFaker = new Faker<Movie>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.Name, f => f.Lorem.Word())
                .RuleFor(
                    u => u.Actors,
                    f => f.Random.ListItems(actors, f.Random.Int(3, 10)).ToList()
                )
                .RuleFor(
                    u => u.MovieStarRatings,
                    f => ratingFaker.Generate(f.Random.Int(10, 100)).ToList()
                );

            var fakeMovies = movieFaker.Generate(10);
            await _movieRepository.AddRangeAsync(fakeMovies);
        }
    }
}
