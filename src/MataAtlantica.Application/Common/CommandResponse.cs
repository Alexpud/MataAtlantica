using MataAtlantica.Domain.Models.Fornecedores;

namespace MataAtlantica.Application.Common;

public record class CommandResponse<TValue> : BaseResponse
{
    public TValue Value { get; private set; }
    public CommandResponse() { }
    public CommandResponse(TValue value) : base()
    {
        Value = value;
    }

    public void SetValue(TValue value) => Value = value;
}
