using FluentResults;

namespace MataAtlantica.Application.Common;

public record class BaseResponse
{
    public IEnumerable<IError> Errors { get; set; } = Enumerable.Empty<IError>();
    public BaseResponse() { }

    public void WithErrors(IEnumerable<IError> errors) 
        => Errors = Errors.Concat(errors);
}
