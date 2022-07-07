using System;


namespace Framework.DependencyInjection;

internal interface IServiceCreator
{
    object CreateService(Type type, IDiContainer container);
}