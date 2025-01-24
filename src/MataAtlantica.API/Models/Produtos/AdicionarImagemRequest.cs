namespace MataAtlantica.API.Presentation.Controllers;

public partial class ProdutoImagensController
{
    public class AdicionarImagemRequest
    {
        public IFormFile File { get; set; }
        public int Ordem { get; set; }
    }
}
