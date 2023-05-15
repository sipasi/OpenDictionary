using System.Threading.Tasks;

using Microsoft.Maui.Controls;

using OpenDictionary.MauiProject;

namespace OpenDictionary.Services.Navigations;

internal sealed class NavigationService : INavigationService
{
    public NavigationService()
    {
        Shell.Current.Navigating += Navigating;
    }

    public Task GoToAsync(string route) => Shell.Current.GoToAsync(route);
    public Task GoToAsync(string route, string parameter, string value) => Shell.Current.GoToAsync($"{route}?{parameter}={value}");
    public Task GoBackAsync() => Shell.Current.GoToAsync("..");

    private void Navigating(object? sender, ShellNavigatingEventArgs e)
    {
        if (Shell.Current is not AppShell shell)
        {
            return;
        }

        if (e is { CanCancel: false, Source: not ShellNavigationSource.Pop })
        {
            return;
        }

        if (shell.CurrentPage is Views.Pages.SettingsPage)
        {
            e.Cancel();

            shell.SetListPage();
        }
    }
}