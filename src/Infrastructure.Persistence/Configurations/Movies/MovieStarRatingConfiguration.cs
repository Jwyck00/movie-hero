using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Movies;

public class MovieStarRatingConfiguration
    : IEntityTypeConfiguration<Domain.Entities.MovieStarRating>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.MovieStarRating> builder)
    {
        builder.Property(e => e.Rate);
        builder.HasOne(e => e.Movie).WithMany(e => e.MovieStarRatings).HasForeignKey(x => x.MovieId);
    }
}
