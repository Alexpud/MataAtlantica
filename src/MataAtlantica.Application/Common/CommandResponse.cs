using MataAtlantica.Domain.Models.Fornecedores;
using MataAtlantica.Domain.Models.Produtos;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MataAtlantica.Application.Common;

public interface ICommandResponse
{
    object GetValue();
}
public record class CommandResponse<TValue> : BaseResponse, ICommandResponse
{
    public TValue Value { get; private set; }
    public CommandResponse() { }

    public CommandResponse(TValue value) : base()
    {
        Value = value;
    }

    public void SetValue(TValue value) => Value = value;
    public object GetValue()
    {
        return Value;
    }
}

public record class CommandResponse : BaseResponse
{
    public CommandResponse() { }
}
