using System;
using System.Threading.Tasks;

using Microsoft.Maui.ApplicationModel;

using OpenDictionary.ExternalAppTranslation;

namespace OpenDictionary.Services.ExternalAppTranslation;

internal sealed class AppOrBrowserLauncher(ILauncher launcher) : IAppOrBrowserLauncher
{
    public async ValueTask OpenAsync(Uri uri)
    {
        await launcher.OpenAsync(uri);
    }
}