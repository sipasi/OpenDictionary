using ProjectWord.Collections.Storages;

using Xamarin.Forms;

namespace ProjectWord.Services.Collections
{
    public static class StorageService<T>
    {
        public static IStorage<T> Storage => DependencyService.Get<IStorage<T>>();
    }
}