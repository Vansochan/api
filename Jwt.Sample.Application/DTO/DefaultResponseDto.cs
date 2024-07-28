namespace Jwt.Sample.Application.DTO;

public record DefaultResponseDto<T>
{
    public bool IsSuccess => Code == "ok";
    public string? Code { get; set; }
    public string? Message { get; set; }
    public string? CorrelationId { get; set; }
    public T? Data { get; set; }
}