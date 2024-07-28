using Jwt.Sample.Domain.Entities;
using Jwt.Sample.Infrastructure.SqlServer.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Sample.Infrastructure.SqlServer.Repositories;

public class DefaultDbContext(DbContextOptions<DefaultDbContext> options) : DbContext(options)
{
    public DbSet<PartnerEntity> Partners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PartnerEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}