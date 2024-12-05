﻿using FluentResults;
using MataAtlantica.Application.Produtos.Common;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Models;
using MataAtlantica.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MataAtlantica.Application.Produtos.AdicionarThumbnail;

public record AdicionarProdutoImagemCommand : IRequest<Result>
{
    public string ProdutoId { get; set; }
    public IFormFile ArquivoImagem { get; set; }
    public int Ordem { get; set; }
}

public record AdicionarProdutoThumbnailCommand : AdicionarProdutoImagemCommand { }



public partial class AdicionarProdutoThumbnailCommandHandler(
    IFileStorageService fileStorageService,
    ProdutoService produtoService) : IRequestHandler<AdicionarProdutoThumbnailCommand, Result>
{
    private readonly IFileStorageService _fileStorageService = fileStorageService;
    private readonly ProdutoService _produtoService = produtoService;

    public async Task<Result> Handle(AdicionarProdutoThumbnailCommand request, CancellationToken cancellationToken)
    {
        var result = await _produtoService.AdicionarThumbnail(new AdicionarImagemProdutoDto(request.ProdutoId, request.Ordem));
        if (result.IsFailed)
            return result;

        await _fileStorageService.UploadFile(CreateFileUploadDto(request, TipoImagem.Thumbnail));
        return Result.Ok();
    }

    private FileUploadDto CreateFileUploadDto(AdicionarProdutoImagemCommand command, TipoImagem tipoImagem)
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