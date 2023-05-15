using System.Diagnostics;

namespace OpenDictionary.ExternalAppTranslation.GoogleServices;

public sealed class GoogleExternalTranslator(IAppOrBrowserLauncher launcher, ITranslatorUrlMaker urlMaker) : IExternalTranslator
{
    public async ValueTask Open(string text, TranslationOptions options)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        try
        {
            Uri uri = urlMaker.Make(text, options);

            await launcher.OpenAsync(uri);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
        }
    }
}