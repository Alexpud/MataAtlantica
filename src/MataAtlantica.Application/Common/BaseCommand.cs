using FluentValidation.Results;

namespace MataAtlantica.Application.Common;

public abstract record BaseCommand
{
    public ValidationResult ValidationResult { get; set; }

    public virtual ValidationResult Validate()
    {
        return new ValidationResult();
    }
}