using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.Ecommerce.Core.Domain.Entity;
using Sample.Ecommerce.Order.Core.Products;

namespace Sample.Ecommerce.Order.Infra;

internal class ContextEcommerce : DbContext, IUnitOfWork
{
    private readonly ILoggerFactory _loggerFactory;

    public DbSet<Product> Products { get; set; }

    public ContextEcommerce(
        DbContextOptions<ContextEcommerce> options, 
        ILoggerFactory loggerFactory) 
        : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            modelBuilder.Entity(entity.ClrType)
                .Ignore(nameof(Entity.IsDeleted));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
           .UseLoggerFactory(_loggerFactory)
           .EnableSensitiveDataLogging(true)
           .EnableDetailedErrors(true)
           .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

        base.OnConfiguring(optionsBuilder);
    }

    public Task SaveChangesEntity(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(cancellationToken);
    }
}
