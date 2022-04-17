namespace Framework.DependencyInjection
{
    public static class ServiceDescriptorExtension
    {
        public static bool IsSingleton(this ServiceDescriptor descriptor) => descriptor.Lifetime == Lifetime.Singleton;
        public static bool IsTransient(this ServiceDescriptor descriptor) => descriptor.Lifetime == Lifetime.Transient;
        public static bool IsScoped(this ServiceDescriptor descriptor) => descriptor.Lifetime == Lifetime.Scoped;
    }
}