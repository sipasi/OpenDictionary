using System.Linq;

using ProjectWord.AppDatabase;
using ProjectWord.Collections.Storages;
using ProjectWord.Models;

using Xamarin.Forms;

namespace ProjectWord.Services.Configurators
{
    internal static class DatabaseConfigurator
    {
        public static void Configure()
        {
            var path = DependencyService.Get<IDatabasePath>();

            DatabaseContext context = new DatabaseContext(path.Path);

            context.Database.EnsureCreated();

            DependencyService.Register<IStorage<Word>, WordStorage>();
            DependencyService.Register<IStorage<WordGroup>, WordGroupStorage>();

            if (context.WordGroups.Any() == false)
            {
                WordGroup[] groups = WordGroupCreator.CreateRange(count: 10);

                context.WordGroups.AddRange(groups);

                context.SaveChanges();
            }
        }
    }
}
