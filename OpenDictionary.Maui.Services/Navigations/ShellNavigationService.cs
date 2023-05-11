using System.Threading.Tasks;

using Microsoft.Maui.Controls;

namespace OpenDictionary.Services.Navigations;

internal class ShellNavigationService : INavigationService
{
    public virtual Task GoToAsync(string route) => Shell.Current.GoToAsync(route);

    public virtual Task GoToAsync(string route, string parameter, string value) => Shell.Current.GoToAsync($"{route}?{parameter}={value}");

    public virtual Task GoBackAsync() => Shell.Current.GoToAsync("..");
}