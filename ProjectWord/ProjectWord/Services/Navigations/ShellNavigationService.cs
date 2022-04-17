using System.Threading.Tasks;

using Xamarin.Forms;

namespace OpenDictionary.Services.Navigations
{
    internal class ShellNavigationService : INavigationService
    {
        public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);

        public Task GoToAsync(string route, string parameter, string value) => Shell.Current.GoToAsync($"{route}?{parameter}={value}");

        public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    }
}