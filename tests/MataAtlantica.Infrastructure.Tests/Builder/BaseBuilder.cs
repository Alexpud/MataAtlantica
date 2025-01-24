namespace MataAtlantica.Infrastructure.Tests.Builder;

public abstract class BaseBuilder<TClass, TBuilder>
    where TClass : class
    where TBuilder : BaseBuilder<TClass, TBuilder>
{
    protected TClass Object;

    public virtual TBuilder BuildDefault()
    {
        return (TBuilder)this;
    }

    public virtual TClass Create()
        => Object;
}
