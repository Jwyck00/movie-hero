using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Actor;

public class ActorConfiguration : IEntityTypeConfiguration<Domain.Entities.Actor>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Actor> builder)
    {
        builder.Property(e => e.Name);
    }
}
