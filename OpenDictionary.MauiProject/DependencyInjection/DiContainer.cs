
using Framework.DependencyInjection;

using OpenDictionary.AppDatabase.Configurators;
using OpenDictionary.DependencyInjection.Extensions;

namespace OpenDictionary.DependencyInjection;

internal class DiContainer
{
    private static IDiContainer container;

    public static void Init()
    {
        ServiceBuilder builder = new ServiceBuilder();

        builder
            .ConfigureViewModels()
            .ConfigureDialogs()
            .ConfigureToastMessages()
            .ConfigureNavigation()
            .ConfigureOnlineDictionary()
            .ConfigureIO()
            .ConfigureAudio()
            .ConfigureDatabase();

        IDiContainer container = builder.Build();

        DatabaseConfigurator.Configure(container);

        DiContainer.container = container;
    }

    public static T Get<T>() where T : class => container.Get<T>();
}