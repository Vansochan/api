using System.Net;
using Api.Shared.Constants;

namespace Jwt.Sample.Application.Common;

public static class ResultResponse
{
    public static DefaultResult<T> SuccessResult<T>(T result, string? code = null, string? message = null,
        string? correlationId = null)
    {
        return new()
        {
            StatusCode = HttpStatusCode.OK,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Success : code,
                Message = message,
                CorrelationId = correlationId,
                Data = result
            }
        };
    }

    public static async Task<DefaultResult<T>> SuccessResultAsync<T>(T result, string? code = null,
        string? message = null, string? correlationId = null)
    {
        return await Task.FromResult(new DefaultResult<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Success : code,
                Message = message ?? "Accepted",
                CorrelationId = correlationId,
                Data = result
            }
        });
    }

    public static async Task<DefaultResult<T>> SuccessResultAsync<T>(string? code = null, string? message = null,
        string? correlationId = null)
    {
        return await Task.FromResult(new DefaultResult<T>()
        {
            StatusCode = HttpStatusCode.OK,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Success : code,
                Message = message,
                CorrelationId = correlationId
            }
        });
    }

    public static DefaultResult<T> FailedResult<T>(string? code = null, string? message = null,
        string? correlationId = null)
    {
        return new()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Rejected : code,
                Message = message,
                CorrelationId = correlationId
            }
        };
    }

    public static async Task<DefaultResult<T>> FailedResultAsync<T>(string? code = null, string? message = null,
        string? correlationId = null)
    {
        return await Task.FromResult(new DefaultResult<T>()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Rejected : code,
                Message = message,
                CorrelationId = correlationId
            }
        });
    }

    public static DefaultResult<T> FailedResult<T>(T result, string? code = null, string? message = null,
        string? correlationId = null)
    {
        return new()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Response = new()
            {
                Code = string.IsNullOrWhiteSpace(code) ? DefaultResponseCode.Rejected : code,
                Message = message,
                CorrelationId = correlationId,
                Data = result
            }
        };
    }
}