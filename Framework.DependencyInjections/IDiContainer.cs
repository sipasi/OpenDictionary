using System;


namespace Framework.DependencyInjection
{
    public interface IDiContainer
    {
        T Get<T>() where T : class;
        object Get(Type type);
    }
}