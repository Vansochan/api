using Jwt.Sample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jwt.Sample.Infrastructure.SqlServer.Configuration;

public class PartnerEntityConfiguration : IEntityTypeConfiguration<PartnerEntity>
{
    public void Configure(EntityTypeBuilder<PartnerEntity> builder)
    {
        builder.Property(p => p.Id).IsRequired().HasDefaultValue(Guid.NewGuid());
        builder.Property(p => p.Name).HasMaxLength(250);
        builder.Property(p => p.SecretKey);
        builder.Property(p => p.ClientId);
        builder.Property(p => p.ValidateAt).IsRequired();
        builder.Property(p => p.Status).IsRequired().HasDefaultValue(true);
        builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        builder.Property(p => p.UpdatedAt);
        builder.Property(p => p.Roles);
    }
}