using MataAtlantica.API.Presentation.Options;
using MataAtlantica.Domain.Abstract.Services;
using MataAtlantica.Domain.Models;
using Microsoft.Extensions.Options;

namespace MataAtlantica.API.Infrastructure.Services;

public class FileStorageService(FileStorageClientFactory factory) : IFileStorageService
{
    public async Task UploadFile(FileUploadDto model)
    {
        var client = factory.GetClient();
        await client.UploadFile(model);
    }
}

public class FileStorageClientFactory(IOptions<FilesOptions> options)
{
    public IFileStorageClient GetClient()
    {
        return options.Value switch
        {
            { UseLocalStorage: true } => new LocalFileStorageClient()
        };
    }
}
