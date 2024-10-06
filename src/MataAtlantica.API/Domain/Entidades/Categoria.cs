namespace MataAtlantica.API.Domain.Entidades;

public class Categoria
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string CategoriaPaiId { get; private set; }
    public Categoria CategoriaPai { get; private set; }

    public void SetCategoriaPai(Categoria categoriaPai)
    {
        CategoriaPai = categoriaPai;
        CategoriaPaiId = categoriaPai.Id;
    }
}
