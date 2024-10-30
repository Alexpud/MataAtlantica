using FluentResults;
using System.Net;

namespace MataAtlantica.API.Presentation.Models;

public class BadRequestResponse
{
    public int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    public List<Error> Errors { get; private set; } = new();
    public string Message { get; set; }

    public void AddError(IError error)
    {
        if (error.HasMetadataKey("ErrorCode"))
            Errors.Add(new Error((string)error.Metadata["ErrorCode"], error.Message));

        throw new NotImplementedException("Esse cenario ainda não foi pensado");
    }
}

public record struct Error(string Code, string Message);
