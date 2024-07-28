using System.ComponentModel.DataAnnotations.Schema;

namespace Jwt.Sample.Domain.Entities;

[Table("Partners", Schema = "Authorize")]
public class PartnerEntity : EntityBase
{
    public string? Name { get; set; }
    public string? SecretKey { get; set; }
    public string? ClientId { get; set; }
    public DateTimeOffset ValidateAt { get; set; }
    public string? Roles { get; set; }
}