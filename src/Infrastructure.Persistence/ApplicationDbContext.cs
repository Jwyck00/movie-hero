using Domain.Common.Interfaces;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DbConfiguration.GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(assembly);
        modelBuilder.AddRestrictDeleteBehaviorConvention();
        modelBuilder.AddPluralizingTableNameConvention();

        var configureAssembly = typeof(ApplicationDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(configureAssembly);

        base.OnModelCreating(modelBuilder);
    }
}
