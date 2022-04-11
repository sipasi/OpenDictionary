
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using ProjectWord.Collections.Storages;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    public abstract class StorageViewModel : ViewModel
    {
        private bool isBusy = false;

        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        public IStorage<T> GetStorage<T>() => DependencyService.Get<IStorage<T>>();

        protected abstract Task Load();

        public async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                await Load();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}