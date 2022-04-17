using System;
using System.Linq;
using System.Reflection;


namespace Framework.DependencyInjection
{

    internal class ConstructorResolver : IConstructorResolver
    {
        public ConstructorInfo GetConstructor(Type type)
        {
            if (type.IsAbstract || type.IsInterface)
            {
                throw new Exception($"Can't create instance from abstract class or interface.\n{type.Name}");
            }

            ConstructorInfo constructor = type.GetConstructors().First();

            return constructor;
        }
    }
}