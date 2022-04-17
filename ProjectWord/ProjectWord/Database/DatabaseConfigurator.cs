using System.Linq;

using Framework.DependencyInjection;

using OpenDictionary.Models;

namespace OpenDictionary.AppDatabase.Configurators
{
    internal static class DatabaseConfigurator
    {
        public static void Configure(IDiContainer container)
        {
            var path = container.Get<IDatabasePath>();

            DatabaseContext context = new DatabaseContext(path.Path);

            context.Database.EnsureCreated();

            if (context.WordGroups.Any() == false)
            {
                WordGroup[] groups = WordGroupCreator.CreateRange(count: 4);

                context.WordGroups.AddRange(groups);

                context.SaveChanges();
            }
        }
    }
}