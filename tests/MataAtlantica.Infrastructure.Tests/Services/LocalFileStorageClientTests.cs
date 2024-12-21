using MataAtlantica.Domain.Models;
using MataAtlantica.Infrastructure.Services;
using System.Text;

namespace MataAtlantica.Infrastructure.Tests.Services;

public class LocalFileStorageClientTests
{
    private readonly LocalFileStorageClient _sut;
    public LocalFileStorageClientTests()
    {
        _sut = new LocalFileStorageClient();
    }

    [Fact]
    public async Task UploadFile_EscreverArquivoNoDiretorio()
    {
        // Arrange
        var idNovoArquivo = "arquivo novo";
        var caminhoNovoArquivo = "{produto}";
        var fileUpload = new FileUploadDto(caminhoNovoArquivo, idNovoArquivo, new MemoryStream());

        // Act
        await _sut.UploadFile(fileUpload);

        // Assert
        var caminhoDiretorioTeste = Path.Combine(Path.GetTempPath(), caminhoNovoArquivo);
        var caminhoEmDisco = Path.Combine(caminhoDiretorioTeste, idNovoArquivo);
        Assert.True(File.Exists(caminhoEmDisco));
        Directory.Delete(caminhoDiretorioTeste, true);
    }

    [Fact]
    public async Task UploadFile_DeveRemoverSubstituirArquivoNoDiretorio_QuandoTiverMesmoNome()
    {
        // Arrange
        var tempPath = Path.GetTempPath();
        var nomeArquivo = "arquivo antigo";

        var path = Path.Combine(tempPath, "temporario");
        Directory.CreateDirectory(path);

        var createdFilePath = Path.Combine(path, nomeArquivo);
        var textoAntigo = "sou texto antigo";
        using (var createdFile = File.Create(createdFilePath))
        {
            createdFile.Write(Encoding.ASCII.GetBytes(textoAntigo));
        };

        var caminhoNovoArquivo = Path.Combine(tempPath, "temporario");
        var textoNovo = "sou texto novo";
        using var stream = new MemoryStream();
        stream.Write(Encoding.ASCII.GetBytes(textoNovo));
        var fileUpload = new FileUploadDto(caminhoNovoArquivo, nomeArquivo, stream);

        // Act
        await _sut.UploadFile(fileUpload);

        // Assert
        var caminhoDiretorioTeste = Path.Combine(Path.GetTempPath(), caminhoNovoArquivo);
        var caminhoEmDisco = Path.Combine(caminhoDiretorioTeste, nomeArquivo);
        Assert.True(File.Exists(caminhoEmDisco));
        Assert.Equal(textoNovo, File.ReadAllText(caminhoEmDisco));
        Directory.Delete(caminhoDiretorioTeste, true);
    }
}
