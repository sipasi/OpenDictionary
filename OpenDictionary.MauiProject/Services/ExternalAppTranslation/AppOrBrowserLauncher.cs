using System;
using System.Threading.Tasks;

using Microsoft.Maui.ApplicationModel;

using OpenDictionary.ExternalAppTranslation;

namespace OpenDictionary.Services.ExternalAppTranslation;

internal sealed class AppOrBrowserLauncher : IAppOrBrowserLauncher
{
    private readonly ILauncher launcher;

    public AppOrBrowserLauncher(ILauncher launcher) => this.launcher = launcher;

    public async ValueTask OpenAsync(Uri uri)
    {
        await launcher.OpenAsync(uri);
    }
}