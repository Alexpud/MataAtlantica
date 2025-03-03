using FluentResults;

namespace MataAtlantica.Application.Common;

public record class BaseResponse
{
    public IEnumerable<IError> Errors { get; set; }
    public BaseResponse() { }
}
