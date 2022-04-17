#nullable enable

using System;
using System.Diagnostics;

using NUnit.Framework;

namespace Framework.DependencyInjection
{
    [DebuggerDisplay(value: "Lifetime = {Lifetime}, ServiceType = {ServiceType.Name}, ImplementationType = {ImplementationType.Name}")]
    public class ServiceDescriptor
    {
        public delegate object ServiceFactory();

        public Lifetime Lifetime { get; }
        public Type ServiceType { get; }
        public Type ImplementationType { get; }
        public object? Instance { get; internal set; }

        public ServiceFactory? Factory { get; }

        public ServiceDescriptor(Lifetime lifetime, Type service, Type? implementation = null, object? instance = null, ServiceFactory? factory = null)
        {
            Assert.IsNotNull(service);

            Lifetime = lifetime;
            ServiceType = service;
            ImplementationType = implementation ?? service;
            Instance = instance;
            Factory = factory;
        }
    }
}