
using Framework.DependencyInjection;

namespace OpenDictionary.AppDatabase.Configurators
{
    internal static class DatabaseConfigurator
    {
        public static void Configure(IDiContainer container)
        {
            var path = container.Get<IDatabasePath>();

            DatabaseContext context = new DatabaseContext(path.Path);

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}