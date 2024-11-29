using MataAtlantica.API.Domain.Models;

namespace MataAtlantica.API.Domain.Abstract.Services;

public interface IFileStorageService
{
    Task UploadFile(FileUploadDto model);
}
