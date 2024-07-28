using System.ComponentModel.DataAnnotations;

namespace Jwt.Sample.Domain.Entities;

public abstract class EntityBase
{
    [Key] public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool Status { get; set; }
}