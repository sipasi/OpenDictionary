using System;
using System.Reflection;


namespace Framework.DependencyInjection
{
    internal interface IConstructorResolver
    {
        ConstructorInfo GetConstructor(Type type);
    }
}