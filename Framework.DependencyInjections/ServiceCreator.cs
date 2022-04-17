using System;
using System.Reflection;

namespace Framework.DependencyInjection
{
    internal class ServiceCreator : IServiceCreator
    {
        private readonly IConstructorResolver resolver;

        public ServiceCreator(IConstructorResolver resolver)
        {
            this.resolver = resolver;
        }

        public object CreateService(Type type, IDiContainer container)
        {
            ConstructorInfo constructor = resolver.GetConstructor(type);

            ParameterInfo[] parameters = constructor.GetParameters();

            int length = parameters.Length;

            if (length == 0)
            {
                return Activator.CreateInstance(type)!;
            }

            return CreateWithParameters(type, parameters, container);
        }

        private object CreateWithParameters(Type type, ParameterInfo[] parameters, IDiContainer container)
        {
            int length = parameters.Length;

            object[] values = new object[length];

            for (int i = 0; i < length; i++)
            {
                ParameterInfo parameter = parameters[i];

                object value = container.Get(parameter.ParameterType);

                values[i] = value;
            }

            return Activator.CreateInstance(type, args: values)!;
        }
    }
}