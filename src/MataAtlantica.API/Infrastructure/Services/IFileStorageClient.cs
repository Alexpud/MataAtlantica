using MataAtlantica.Domain.Models;

namespace MataAtlantica.API.Infrastructure.Services;

public interface IFileStorageClient
{
    Task UploadFile(FileUploadDto dto);
}

public class LocalFileStorageClient : IFileStorageClient
{
    public async Task UploadFile(FileUploadDto dto)
    {
        var tempPath = Path.GetTempPath();
        var diretorioArquivo = Path.Combine(tempPath, dto.CaminhoArquivo);
        
        if (!Directory.Exists(diretorioArquivo))
            Directory.CreateDirectory(diretorioArquivo);

        var caminhoArquivo = Path.Combine(diretorioArquivo, dto.NomeArquivo);
        using (var file = File.Create(caminhoArquivo))
        {
            dto.File.Seek(0, SeekOrigin.Begin);
            await dto.File.CopyToAsync(file);
        }
    }
}
