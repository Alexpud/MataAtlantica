using FluentResults;
using FluentValidation;
using MataAtlantica.API.Domain.Abstract.Services;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Helpers;
using AdicionarImagemProdutoDto = MataAtlantica.API.Application.Models.AdicionarImagemProdutoDto;

namespace MataAtlantica.API.Application.Services;

public partial class ImagensProdutoService(
    IValidator<AdicionarImagemProdutoDto> validator,
    IFileStorageService fileStorageService,
    ProdutoService produtoService)
{
    private readonly IValidator<AdicionarImagemProdutoDto> _adicionarThumbnailsValidator = validator;
    private readonly ProdutoService _produtoService = produtoService;

    public async Task<Result> AdicionarThumbnail(AdicionarImagemProdutoDto model)
    {
        var result = await _adicionarThumbnailsValidator.ValidateAsync(model);
        if (!result.IsValid)
            return Result.Fail(result.GetErrors());

        var dto = new Domain.Models.AdicionarImagemProdutoDto(model.ProdutoId, model.Ordem);
        await _produtoService.AdicionarThumbnail(dto);

        var fileUploadDto = CreateFileUploadDto(model, TipoImagem.Thumbnail);
        await fileStorageService.UploadFile(fileUploadDto);
        return Result.Ok();
    }

    public async Task<Result> AdicionarImagemIlustrativa(AdicionarImagemProdutoDto model)
    {
        var result = await _adicionarThumbnailsValidator.ValidateAsync(model);
        if (!result.IsValid)
            return Result.Fail(result.GetErrors());

        var dto = new Domain.Models.AdicionarImagemProdutoDto(model.ProdutoId, model.Ordem);
        await _produtoService.AdicionarImagemIlustrativa(dto);

        var fileUploadDto = CreateFileUploadDto(model, TipoImagem.Ilustrativa);
        await fileStorageService.UploadFile(fileUploadDto);
        return Result.Ok();
    }

    private FileUploadDto CreateFileUploadDto(AdicionarImagemProdutoDto model, TipoImagem tipoImagem)
    {
        var filePath = Path.Combine("imagens", model.ProdutoId, tipoImagem.ToString());
        var fileName = $"{model.Ordem}.{GetFileExtension(model.ArquivoImagem)}";
        var memoryStream = new MemoryStream();
        model.ArquivoImagem.CopyTo(memoryStream);
        return new FileUploadDto(filePath, fileName, memoryStream);
    }

    private string GetFileExtension(IFormFile file)
        => file.FileName.Split(".")[1];
}
