
using Framework.DependencyInjection;

using OpenDictionary.AppDatabase.Configurators;
using OpenDictionary.DependencyInjection.Extensions;

namespace OpenDictionary.DependencyInjection
{
    internal sealed class DiContainer
    {
        private static IDiContainer container;

        private DiContainer() { }

        public static void Init()
        {
            ServiceBuilder builder = new ServiceBuilder();

            builder
                .ConfigureViewModels()
                .ConfigureDialogs()
                .ConfigureToastMessages()
                .ConfigureOnlineDictionary()
                .ConfigureNavigation()
                .ConfigureIO()
                .ConfigureAudio()
                .ConfigureDatabase();

            IDiContainer container = builder.Build();

            DatabaseConfigurator.Configure(container);

            DiContainer.container = container;
        }

        public static T Get<T>() where T : class => container.Get<T>();
    }
}