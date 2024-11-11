using FluentResults;
using FluentValidation;
using MataAtlantica.API.Domain.Abstract.Services;
using MataAtlantica.API.Domain.Models;
using MataAtlantica.API.Domain.Services;
using MataAtlantica.API.Helpers;

namespace MataAtlantica.API.Application.Services;

public class ImagensProdutoService(
    IValidator<Models.AdicionarThumbnailProdutoDto> validator,
    IFileStorageService fileStorageService,
    ProdutoService produtoService)
{
    private readonly IValidator<Models.AdicionarThumbnailProdutoDto> _adicionarThumbnailsValidator = validator;
    private readonly ProdutoService _produtoService = produtoService;

    public async Task<Result> AdicionarThumbnails(Models.AdicionarThumbnailProdutoDto model)
    {
        var result = await _adicionarThumbnailsValidator.ValidateAsync(model);
        if (!result.IsValid)
            return Result.Fail(result.GetErrors());

        var dto = new Domain.Models.AdicionarThumbnailProdutoDto(model.ProdutoId, model.Ordem);
        await _produtoService.AdicionarThumbnail(dto);

        var fileUploadDto = CreateFileUploadDto(model);
        await fileStorageService.UploadFile(fileUploadDto);
        return Result.Ok();
    }

    private FileUploadDto CreateFileUploadDto(Models.AdicionarThumbnailProdutoDto model)
    {
        var filePath = Path.Combine("imagens", model.ProdutoId, "thumbnails");
        var fileName = $"{model.Ordem}.{GetFileExtension(model.Thumbnail)}";
        var memoryStream = new MemoryStream();
        model.Thumbnail.CopyTo(memoryStream);
        return new FileUploadDto(filePath, fileName, memoryStream);
    }

    private string GetFileExtension(IFormFile file)
        => file.FileName.Split(".")[1];
}
