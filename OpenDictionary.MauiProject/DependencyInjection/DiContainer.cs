
using Framework.DependencyInjection;

using OpenDictionary.AppDatabase.Configurators;
using OpenDictionary.DependencyInjection.Extensions;

namespace OpenDictionary.DependencyInjection;

internal class DiContainer
{
    private static readonly IDiContainer container = Build();

    public static void Init()
    {
        DatabaseConfigurator.Configure(container);
    }

    private static IDiContainer Build()
    {
        ServiceBuilder builder = new ServiceBuilder();

        builder
            .ConfigureViewModels()
            .ConfigureMessagesDialogs()
            .ConfigureNavigation()
            .ConfigureOnlineDictionary()
            .ConfigureIO()
            .ConfigureAudio()
            .ConfigureDataTransfer()
            .ConfigureDatabase();

        IDiContainer container = builder.Build();

        return container;
    }

    public static T Get<T>() where T : class => container.Get<T>();
}