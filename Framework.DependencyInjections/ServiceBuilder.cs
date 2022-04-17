#nullable enable

using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace Framework.DependencyInjection
{
    public class ServiceBuilder
    {
        private readonly List<ServiceDescriptor> descriptors;

        public readonly Builder singleton;
        public readonly Builder transient;

        public ServiceBuilder()
        {
            descriptors = new List<ServiceDescriptor>();
            singleton = new Builder(descriptors, Lifetime.Singleton);
            transient = new Builder(descriptors, Lifetime.Transient);
        }

        public IDiContainer Build()
        {
            var pairs = descriptors.ToDictionary(key => key.ServiceType);

            DiContainer container = new DiContainer(pairs);

            return container;
        }
    }

    public readonly struct Builder
    {
        private readonly List<ServiceDescriptor> descriptors;
        private readonly Lifetime lifetime;

        public Builder(List<ServiceDescriptor> descriptors, Lifetime lifetime)
        {
            this.descriptors = descriptors;
            this.lifetime = lifetime;
        }

        public Builder Add<T>() where T : class
        {
            return AddService<T, T>();
        }
        public Builder Add<T>(T instance) where T : class
        {
            Assert.IsNotNull(instance, $"Instance can't be null. Type[{typeof(T)}]");

            return AddService<T, T>(instance);
        }
        public Builder Add<TService, TImplementation>()
           where TService : class
           where TImplementation : class, TService
        {
            return AddService<TService, TImplementation>();
        }
        public Builder Add<TService, TImplementation>(TImplementation instance)
            where TService : class
            where TImplementation : class, TService
        {
            Assert.IsNotNull(instance, $"Instance can't be null. Type[{typeof(TService)}]");

            return AddService<TService, TImplementation>(instance);
        }

        private Builder AddService<TService, TImplementation>(TImplementation? instance = null)
            where TService : class
            where TImplementation : class, TService
        {
            var service = typeof(TService);
            var implementation = typeof(TImplementation);

            ServiceDescriptor descriptor = new ServiceDescriptor(lifetime, service, implementation, instance);

            descriptors.Add(descriptor);

            return this;
        }
    }
}