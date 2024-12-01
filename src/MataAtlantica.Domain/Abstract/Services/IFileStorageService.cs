using MataAtlantica.Domain.Models;

namespace MataAtlantica.Domain.Abstract.Services;

public interface IFileStorageService
{
    Task UploadFile(FileUploadDto model);
}
