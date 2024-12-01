using FluentResults;
using FluentValidation.Results;

namespace MataAtlantica.API.Helpers;

public static class ValidationResultExtensions
{
    public static IEnumerable<IError> GetErrors(this ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
            yield return new Error(error.ErrorMessage).WithMetadata("ErrorCode", error.ErrorCode);
    }
}
