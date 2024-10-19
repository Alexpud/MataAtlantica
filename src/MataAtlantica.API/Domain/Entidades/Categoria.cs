namespace MataAtlantica.API.Domain.Entidades;

public class Categoria : EntidadeBase
{
    public string Nome { get; set; }
    public string CategoriaPaiId { get; private set; }
    public Categoria CategoriaPai { get; private set; }
    public List<Categoria> SubCategorias { get; private set; } = new();

    public Categoria() { }

    public Categoria(string nome) : base()
    {
        Nome = nome;
    }

    public void AdicionarSubCategoria(Categoria categoria)
        => SubCategorias.Add(categoria);

    public void SetCategoriaPai(Categoria categoriaPai) 
        => CategoriaPaiId = categoriaPai.Id;
}