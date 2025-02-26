using FluentValidation.Results;

namespace MataAtlantica.Application.Common;

public abstract class BaseRequest
{
    public ValidationResult ValidationResult { get; set; }

    public virtual ValidationResult Validate()
    {
        return new ValidationResult();
    }
}