namespace MataAtlantica.API.Tests.Builder;

public abstract class BaseBuilder<TClass, TBuilder> 
    where TClass : class, new()
    where TBuilder : BaseBuilder<TClass, TBuilder>
{
    protected TClass Object;

    public virtual TBuilder BuildDefault()
    {
        Object = new();
        return (TBuilder)this;
    }

    public virtual TClass Create()
        => Object;
}
