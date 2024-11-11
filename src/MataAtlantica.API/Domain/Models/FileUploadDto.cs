namespace MataAtlantica.API.Domain.Models;

public class FileUploadDto
{
    public FileUploadDto(string path, string fileName, Stream stream)
    {
        NomeArquivo = fileName;
        CaminhoArquivo = path;
        File = stream;
    }

    public Stream File { get; set; }
    public string CaminhoArquivo { get; set; }
    public string NomeArquivo { get; set; }
}
