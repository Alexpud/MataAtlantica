using FluentResults;
using FluentValidation.Results;
using MataAtlantica.Application.Common;
using MataAtlantica.Application.Produtos.Common;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Models.Produtos;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MataAtlantica.Application.Produtos.AdicionarImagemIlustrativa;

public record AdicionarImagemIlustrativaCommand(string ProdutoId, IFormFile ArquivoImagem, int Ordem) : BaseCommand, IRequest<Result>
{
    public override ValidationResult Validate()
    {
        var validator = new AdicionarImagemIlustrativaCommandValidator();
        return validator.Validate(this);
    }
}

public class CommandHandler(
    IFileStorageService fileStorageService,
    ProdutoService produtoService) : IRequestHandler<AdicionarImagemIlustrativaCommand, Result>
{
    private readonly IFileStorageService _fileStorageService = fileStorageService;
    private readonly ProdutoService _produtoService = produtoService;

    public async Task<Result> Handle(AdicionarImagemIlustrativaCommand request, CancellationToken cancellationToken)
    {
        var result = await _produtoService.AdicionarImagemIlustrativa(new AdicionarImagemProdutoDto(request.ProdutoId, request.Ordem));
        if (result.IsFailed)
            return result;

        await _fileStorageService.UploadFile(CreateFileUploadDto(request, TipoImagem.Ilustrativa));
        return Result.Ok();
    }

    private FileUploadDto CreateFileUploadDto(AdicionarImagemIlustrativaCommand command, TipoImagem tipoImagem)
    {
        var filePath = Path.Combine("imagens", command.ProdutoId, tipoImagem.ToString());
        var fileName = $"{command.Ordem}.{GetFileExtension(command.ArquivoImagem)}";
        var memoryStream = new MemoryStream();
        command.ArquivoImagem.CopyTo(memoryStream);
        return new FileUploadDto(filePath, fileName, memoryStream);
    }

    private string GetFileExtension(IFormFile file)
        => file.FileName.Split(".")[1];
}
