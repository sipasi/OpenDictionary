#nullable enable

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Framework.DependencyInjection
{
    internal class DiContainer : IDiContainer
    {
        private readonly IServiceProvider provider;

        private readonly Dictionary<Type, ServiceDescriptor> container;

        internal DiContainer(Dictionary<Type, ServiceDescriptor> container)
        {
            ConstructorResolver resolver = new ConstructorResolver();

            ServiceCreator creator = new ServiceCreator(resolver);

            provider = new ServiceProvider(creator);

            this.container = container;
        }
        internal DiContainer(IServiceProvider provider, Dictionary<Type, ServiceDescriptor> container)
        {
            this.provider = provider;
            this.container = container;
        }

        public T Get<T>() where T : class
        {
            var type = typeof(T);

            object service = Get(type);

            T result = (service as T)!;

            return result;
        }
        public object Get(Type type)
        {
            if (container.ContainsKey(type) == false)
            {
                string message = $"Container don't have descriptor by type[{type.Name}].";

                if (type.IsGenericType)
                {
                    message += " Generic[";

                    foreach (var item in type.GenericTypeArguments)
                    {
                        message += $" {item.Name}";
                    }

                    message += "]";
                }

                Assert.IsTrue(false, message);
            }

            ServiceDescriptor descriptor = container[type];

            Assert.IsNotNull(descriptor);

            Type implementation = descriptor.ImplementationType;

            Assert.IsNotNull(implementation);

            if (descriptor.IsSingleton())
            {
                return provider.GetSingleton(descriptor, this);
            }
            if (descriptor.IsTransient())
            {
                return provider.GetTransient(descriptor, this);
            }

            throw new NotSupportedException($"The Lifetime serivce[{descriptor.Lifetime}] is not support");
        }
    }
}