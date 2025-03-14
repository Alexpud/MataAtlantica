namespace MataAtlantica.Domain.Entidades;

public abstract class EntidadeBase
{
    public string Id { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    protected EntidadeBase()
    {
        Id = Guid.NewGuid().ToString("D");
    }
}
