namespace Framework.DependencyInjection;

internal interface IServiceProvider
{
    object GetSingleton(ServiceDescriptor descriptor, IDiContainer container);
    object GetTransient(ServiceDescriptor descriptor, IDiContainer container);
}