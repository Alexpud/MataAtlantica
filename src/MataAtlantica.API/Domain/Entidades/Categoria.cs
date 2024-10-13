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

    public bool EhSubCategoria()
        => string.IsNullOrWhiteSpace(CategoriaPaiId);

    public void AdicionarSubCategoria(Categoria categoria)
        => SubCategorias.Add(categoria);

    public void SetCategoriaPai(Categoria categoriaPai) 
        => CategoriaPaiId = categoriaPai.Id;

    public void AdicionarSubCategorias(List<Categoria> subCategorias) 
        => SubCategorias.AddRange(subCategorias);
}

public abstract class EntidadeBase
{
    public string Id { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    protected EntidadeBase()
    {
        Id = Guid.NewGuid().ToString("D");
    }
}
