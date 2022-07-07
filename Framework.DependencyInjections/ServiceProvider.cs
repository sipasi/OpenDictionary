using System;


namespace Framework.DependencyInjection;

internal class ServiceProvider : IServiceProvider
{
    private readonly IServiceCreator creator;

    public ServiceProvider(IServiceCreator creator)
    {
        this.creator = creator;
    }

    public object GetSingleton(ServiceDescriptor descriptor, IDiContainer container)
    {
        Type implementation = descriptor.ImplementationType;

        if (descriptor.Instance == null)
        {
            descriptor.Instance = creator.CreateService(implementation, container);
        }

        return descriptor.Instance!;
    }
    public object GetTransient(ServiceDescriptor descriptor, IDiContainer container)
    {
        Type implementation = descriptor.ImplementationType;

        ServiceDescriptor.ServiceFactory factory = descriptor.Factory!;

        if (factory != null)
        {
            return factory.Invoke();
        }

        return creator.CreateService(implementation, container);
    }
}