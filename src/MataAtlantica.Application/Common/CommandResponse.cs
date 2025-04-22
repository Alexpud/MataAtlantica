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
    public bool EhInvalida => Errors.Any();
    public CommandResponse() { }
}
