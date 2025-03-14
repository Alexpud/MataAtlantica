using FluentResults;
using System.Text.Json.Serialization;

namespace MataAtlantica.Application.Common;

public record class BaseResponse
{
    [JsonIgnore]
    public List<Erro> Errors { get; set; } = new List<Erro>();
    public BaseResponse() { }

    public void WithErrors(IEnumerable<IError> errors) 
        => Errors.AddRange(errors.Select(p => new Erro((string)p.Metadata["ErrorCode"],p.Message)));
}

public record Erro(string Codigo, string Mensagem);