using System.Net;
using Jwt.Sample.Application.DTO;

namespace Jwt.Sample.Application.Common;

public class DefaultResult<T>
{
    public required HttpStatusCode StatusCode { get; set; }
    public DefaultResponseDto<T?>? Response { get; set; }
}
